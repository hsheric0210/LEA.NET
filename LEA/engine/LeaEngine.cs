using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static LEA.Utils.Ops;

namespace LEA.engine
{
	public class LeaEngine : BlockCipher
	{
		private static readonly int BLOCKSIZE = 16;
		private static readonly int[] delta = unchecked(new uint[] { 0xc3efe9db, 0x44626b02, 0x79e27c8a, 0x78df30ec, 0x715ea49e, 0xc785da0a, 0xe04ef22a, 0xe5c40957 }.Select(d => (int)d).ToArray());
		private Mode mode;
		private int rounds;
		protected int[][] roundKeys;
		private int[] block;

		public LeaEngine()
		{
			block = new int[BLOCKSIZE / 4];
		}

		public override void Init(Mode mode, byte[] mk)
		{
			this.mode = mode;
			GenerateRoundKeys(mk);
		}

		public override void Reset()
		{
			block.FillBy((byte)0);
		}

		public override string GetAlgorithmName()
		{
			return "LEA";
		}

		public override int GetBlockSize()
		{
			return BLOCKSIZE;
		}

		public override int ProcessBlock(byte[] @in, int inOff, byte[] @out, int outOff)
		{
			if (@in == null || @out == null)
				throw new ArgumentNullException("in and out should not be null");

			if (@in.Length - inOff < BLOCKSIZE)
				throw new InvalidOperationException("too short input data " + @in.Length + " " + inOff);

			if (@out.Length - outOff < BLOCKSIZE)
				throw new InvalidOperationException("too short output buffer " + @out.Length + " / " + outOff);

			if (mode == Mode.ENCRYPT)
				return EncryptBlock(@in, inOff, @out, outOff);

			return DecryptBlock(@in, inOff, @out, outOff);
		}

		private int EncryptBlock(byte[] @in, int inOff, byte[] @out, int outOff)
		{
			Pack(@in, inOff, block, 0, 16);
			for (var i = 0; i < rounds; ++i)
			{
				block[3] = ROR((block[2] ^ roundKeys[i][4]) + (block[3] ^ roundKeys[i][5]), 3);
				block[2] = ROR((block[1] ^ roundKeys[i][2]) + (block[2] ^ roundKeys[i][3]), 5);
				block[1] = ROL((block[0] ^ roundKeys[i][0]) + (block[1] ^ roundKeys[i][1]), 9);
				++i;
				block[0] = ROR((block[3] ^ roundKeys[i][4]) + (block[0] ^ roundKeys[i][5]), 3);
				block[3] = ROR((block[2] ^ roundKeys[i][2]) + (block[3] ^ roundKeys[i][3]), 5);
				block[2] = ROL((block[1] ^ roundKeys[i][0]) + (block[2] ^ roundKeys[i][1]), 9);
				++i;
				block[1] = ROR((block[0] ^ roundKeys[i][4]) + (block[1] ^ roundKeys[i][5]), 3);
				block[0] = ROR((block[3] ^ roundKeys[i][2]) + (block[0] ^ roundKeys[i][3]), 5);
				block[3] = ROL((block[2] ^ roundKeys[i][0]) + (block[3] ^ roundKeys[i][1]), 9);
				++i;
				block[2] = ROR((block[1] ^ roundKeys[i][4]) + (block[2] ^ roundKeys[i][5]), 3);
				block[1] = ROR((block[0] ^ roundKeys[i][2]) + (block[1] ^ roundKeys[i][3]), 5);
				block[0] = ROL((block[3] ^ roundKeys[i][0]) + (block[0] ^ roundKeys[i][1]), 9);
			}

			Unpack(block, 0, @out, outOff, 4);
			return BLOCKSIZE;
		}

		private int DecryptBlock(byte[] @in, int inOff, byte[] @out, int outOff)
		{
			Pack(@in, inOff, block, 0, 16);
			for (var i = rounds - 1; i >= 0; --i)
			{
				block[0] = ROR(block[0], 9) - (block[3] ^ roundKeys[i][0]) ^ roundKeys[i][1];
				block[1] = ROL(block[1], 5) - (block[0] ^ roundKeys[i][2]) ^ roundKeys[i][3];
				block[2] = ROL(block[2], 3) - (block[1] ^ roundKeys[i][4]) ^ roundKeys[i][5];
				--i;
				block[3] = ROR(block[3], 9) - (block[2] ^ roundKeys[i][0]) ^ roundKeys[i][1];
				block[0] = ROL(block[0], 5) - (block[3] ^ roundKeys[i][2]) ^ roundKeys[i][3];
				block[1] = ROL(block[1], 3) - (block[0] ^ roundKeys[i][4]) ^ roundKeys[i][5];
				--i;
				block[2] = ROR(block[2], 9) - (block[1] ^ roundKeys[i][0]) ^ roundKeys[i][1];
				block[3] = ROL(block[3], 5) - (block[2] ^ roundKeys[i][2]) ^ roundKeys[i][3];
				block[0] = ROL(block[0], 3) - (block[3] ^ roundKeys[i][4]) ^ roundKeys[i][5];
				--i;
				block[1] = ROR(block[1], 9) - (block[0] ^ roundKeys[i][0]) ^ roundKeys[i][1];
				block[2] = ROL(block[2], 5) - (block[1] ^ roundKeys[i][2]) ^ roundKeys[i][3];
				block[3] = ROL(block[3], 3) - (block[2] ^ roundKeys[i][4]) ^ roundKeys[i][5];
			}

			Unpack(block, 0, @out, outOff, 4);
			return BLOCKSIZE;
		}

		private void GenerateRoundKeys(byte[] mk)
		{
			if (mk == null || mk.Length != 16 && mk.Length != 24 && mk.Length != 32)
				throw new ArgumentException("Illegal key");

			var T = new int[8];
			rounds = (mk.Length >> 1) + 16;
			roundKeys = new int[rounds][]; // FIXME: Convert this to multidimensional array (https://stackoverflow.com/questions/72980478/how-to-initialize-a-multidimensional-array)
			for (int i = 0; i < rounds; i++)
				roundKeys[i] = new int[6];
			Pack(mk, 0, T, 0, 16);
			if (mk.Length > 16)
				Pack(mk, 16, T, 4, 8);

			if (mk.Length > 24)
				Pack(mk, 24, T, 6, 8);

			if (mk.Length == 16)
			{
				for (var i = 0; i < 24; ++i)
				{
					var temp = ROL(delta[i & 3], i);
					roundKeys[i][0] = T[0] = ROL(T[0] + ROL(temp, 0), 1);
					roundKeys[i][1] = roundKeys[i][3] = roundKeys[i][5] = T[1] = ROL(T[1] + ROL(temp, 1), 3);
					roundKeys[i][2] = T[2] = ROL(T[2] + ROL(temp, 2), 6);
					roundKeys[i][4] = T[3] = ROL(T[3] + ROL(temp, 3), 11);
				}
			}
			else if (mk.Length == 24)
			{
				for (var i = 0; i < 28; ++i)
				{
					var temp = ROL(delta[i % 6], i);
					roundKeys[i][0] = T[0] = ROL(T[0] + ROL(temp, 0), 1);
					roundKeys[i][1] = T[1] = ROL(T[1] + ROL(temp, 1), 3);
					roundKeys[i][2] = T[2] = ROL(T[2] + ROL(temp, 2), 6);
					roundKeys[i][3] = T[3] = ROL(T[3] + ROL(temp, 3), 11);
					roundKeys[i][4] = T[4] = ROL(T[4] + ROL(temp, 4), 13);
					roundKeys[i][5] = T[5] = ROL(T[5] + ROL(temp, 5), 17);
				}
			}
			else
			{
				for (var i = 0; i < 32; ++i)
				{
					var temp = ROL(delta[i & 7], i & 0x1f);
					roundKeys[i][0] = T[6 * i + 0 & 7] = ROL(T[6 * i + 0 & 7] + temp, 1);
					roundKeys[i][1] = T[6 * i + 1 & 7] = ROL(T[6 * i + 1 & 7] + ROL(temp, 1), 3);
					roundKeys[i][2] = T[6 * i + 2 & 7] = ROL(T[6 * i + 2 & 7] + ROL(temp, 2), 6);
					roundKeys[i][3] = T[6 * i + 3 & 7] = ROL(T[6 * i + 3 & 7] + ROL(temp, 3), 11);
					roundKeys[i][4] = T[6 * i + 4 & 7] = ROL(T[6 * i + 4 & 7] + ROL(temp, 4), 13);
					roundKeys[i][5] = T[6 * i + 5 & 7] = ROL(T[6 * i + 5 & 7] + ROL(temp, 5), 17);
				}
			}
		}

		// utilities
		private static int ROL(int state, int num)
		{
			return state << num | state >> 32 - num;
		}

		private static int ROR(int state, int num)
		{
			return state >> num | state << 32 - num;
		}
	}
}