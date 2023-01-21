using LEA.Extension;
using System.Buffers;

namespace LEA.Mode;

public class CcmMode : BlockCipherModeAE
{
	private byte[] ctr;
	private byte[] mac;
	private byte[] tag;
	private byte[] block;
	private MemoryStream aadBytes;
	private MemoryStream inputBytes;
	private int msglen;
	private int taglen;
	private int noncelen;
	public CcmMode(BlockCipher cipher) : base(cipher)
	{
		ctr = new byte[blockSize];
		mac = new byte[blockSize];
		block = new byte[blockSize];
	}

	public override void Init(OperatingMode mode, ReadOnlySpan<byte> mk, ReadOnlySpan<byte> nonce, int taglen)
	{
		this.mode = mode;
		engine.Init(OperatingMode.Encrypt, mk);
		aadBytes = new MemoryStream();
		inputBytes = new MemoryStream();
		SetTaglen(taglen);
		SetNonce(nonce);
	}

	public override void UpdateAAD(ReadOnlySpan<byte> aad)
	{
		if (aad.Length == 0)
		{
			return;
		}

		aadBytes.Write(aad);
	}

	public override ReadOnlySpan<byte> Update(ReadOnlySpan<byte> msg)
	{
		inputBytes.Write(msg);
		return null;
	}

	public override ReadOnlySpan<byte> DoFinal()
	{
		Close(aadBytes);
		Close(inputBytes);
		if (aadBytes.Length > 0)
		{
			block[0] |= (byte)0x40;
		}

		msglen = inputBytes.ToArray().Length;
		if (mode == OperatingMode.Decrypt)
		{
			msglen -= taglen;
		}

		ToBytes(msglen, block, noncelen + 1, 15 - noncelen);
		engine.ProcessBlock(block, 0, mac, 0);
		Span<byte> output;
		ProcessAAD();
		if (mode == OperatingMode.Encrypt)
		{
			output = new byte[msglen + taglen];
			EncryptData(output, 0);
		}
		else
		{
			output = new byte[msglen];
			DecryptData(output, 0);
		}

		ResetCounter();
		engine.ProcessBlock(ctr, 0, block, 0);
		if (mode == OperatingMode.Encrypt)
		{
			mac.XOR(block);
			Array.Copy(mac, 0, tag, 0, taglen);
			mac[..taglen].CopyTo(output[(output.Length - taglen)..]);
		}
		else
		{
			mac = mac[..taglen].ToArray();
			if (!Array.Equals(tag, mac))
				output.Fill(0);
		}

		return output;
	}

	public override int GetOutputSize(int len)
	{
		var outSize = len + bufferOffset;
		if (mode == OperatingMode.Encrypt)
		{
			return outSize + taglen;
		}

		return outSize < taglen ? 0 : outSize - taglen;
	}

	private void SetNonce(ReadOnlySpan<byte> nonce)
	{
		if (nonce == null)
		{
			throw new ArgumentNullException(nameof(nonce));
		}

		noncelen = nonce.Length;
		if (noncelen is < 7 or > 13)
		{
			throw new ArgumentException("length of nonce should be 7 ~ 13 bytes");
		}


		// init counter
		ctr[0] = (byte)(14 - noncelen);
		nonce.CopyTo(ctr.AsSpan()[1..]);

		// init b0
		var tagfield = (taglen - 2) / 2;
		block[0] = (byte)((tagfield << 3) & 0xff);
		block[0] |= (byte)((14 - noncelen) & 0xff);
		nonce.CopyTo(block.AsSpan()[1..]);
	}

	private void SetTaglen(int taglen)
	{
		if (taglen < 4 || taglen > 16 || (taglen & 0x01) != 0)
		{
			throw new ArgumentException("length of tag should be 4, 6, 8, 10, 12, 14, 16 bytes");
		}

		this.taglen = taglen;
		tag = new byte[taglen];
	}

	private void ResetCounter() => Array.Fill(ctr, (byte)0, noncelen + 1, ctr.Length);

	private void IncreaseCounter()
	{
		var i = ctr.Length - 1;
		while (++ctr[i] == 0)
		{
			--i;
			if (i < noncelen + 1)
			{
				throw new InvalidOperationException("exceed maximum counter");
			}
		}
	}

	private void ProcessAAD()
	{
		var aad = aadBytes.ToArray();
		Array.Fill(block, (byte)0);
		var alen = 0;
		if (aad.Length < 0xff00)
		{
			alen = 2;
			ToBytes(aad.Length, block, 0, 2);
		}
		else
		{
			alen = 6;
			block[0] = (byte)0xff;
			block[1] = (byte)0xfe;
			ToBytes(aad.Length, block, 2, 4);
		}

		if (aad.Length == 0)
		{
			return;
		}

		var i = 0;
		var remained = aad.Length;
		var processed = remained > blockSize - alen ? blockSize - alen : aad.Length;
		i += processed;
		remained -= processed;
		Array.Copy(aad, 0, block, alen, processed);
		mac.XOR(block);
		engine.ProcessBlock(mac, 0, mac, 0);
		while (remained > 0)
		{
			processed = remained >= blockSize ? blockSize : remained;
			mac.XOR(0, mac, 0, aad, i, processed);
			engine.ProcessBlock(mac, 0, mac, 0);
			i += processed;
			remained -= processed;
		}
	}

	private void EncryptData(Span<byte> output, int offset)
	{
		var inIdx = 0;
		var remained = 0;
		var processed = 0;
		var outIdx = offset;
		var input = inputBytes.ToArray();
		remained = msglen;
		while (remained > 0)
		{
			processed = remained >= blockSize ? blockSize : remained;
			mac.XOR(0, mac, 0, input, inIdx, processed);
			engine.ProcessBlock(mac, 0, mac, 0);
			IncreaseCounter();
			engine.ProcessBlock(ctr, 0, block, 0);
			output.XOR(outIdx, block, 0, input, inIdx, processed);
			inIdx += processed;
			outIdx += processed;
			remained -= processed;
		}
	}

	private void DecryptData(Span<byte> output, int offset)
	{
		var i = 0;
		var remained = 0;
		var processed = 0;
		var outIdx = offset;
		var input = inputBytes.ToArray();
		Array.Copy(input, msglen, tag, 0, taglen);
		engine.ProcessBlock(ctr, 0, block, 0);
		tag.XOR(0, block, 0, taglen);
		remained = msglen;
		while (remained > 0)
		{
			processed = remained >= blockSize ? blockSize : remained;
			IncreaseCounter();
			engine.ProcessBlock(ctr, 0, block, 0);
			output.XOR(outIdx, block, 0, input, i, processed);
			mac.XOR(0, mac, 0, output, outIdx, processed);
			engine.ProcessBlock(mac, 0, mac, 0);
			i += processed;
			outIdx += processed;
			remained -= processed;
		}
	}

	private static void Close(IDisposable obj)
	{
		if (obj == null)
		{
			return;
		}

		try
		{
			obj.Dispose();
		}
		catch
		{
		}
	}
}