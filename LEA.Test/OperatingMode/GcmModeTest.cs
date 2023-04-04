using static LEA.BlockCipher;

namespace LEA.Test.OpMode
{
	public class GcmModeTest
	{
		private readonly TestVectorAE[] lea256GcmTestVectors =
		{
		new TestVectorAE
		{
			Key = new byte[] { 0xBB, 0x59, 0x5C, 0xEE, 0xDB, 0xDA, 0x78, 0xFA, 0xD3, 0xD6, 0x17, 0xAB, 0x58, 0xC2, 0x25, 0x4C, 0x57, 0x63, 0xA3, 0xE7, 0x7F, 0x41, 0x4E, 0x5D, 0xFF, 0x59, 0x7B, 0xB5, 0x91, 0x12, 0x46, 0xB5 },
			IV = new byte[] { 0xB7, 0x72, 0xEB, 0xAF, 0x44, 0x92, 0x74, 0x9E, 0x1F, 0x4F, 0x95, 0x72 },
			AAD = Array.Empty<byte>(),
			PlainText = new byte[] { 0x44, 0x43, 0xD4, 0xA7, 0xB, 0xF4, 0xEC, 0x1C },
			CipherText = new byte[] { 0x3E, 0x10, 0x38, 0xF1, 0xE1, 0xEC, 0xD4, 0xC7 },
			Tag = new byte[] { 0xAC, 0x2D, 0x3D, 0xAB, 0xA0, 0xB3, 0x2, 0x4B, 0x6B, 0x8C, 0xB4, 0x2D, 0xC8, 0x3B, 0x6D, 0x4F },
		},
		new TestVectorAE
		{
			Key = new byte[] { 0xAD, 0x4A, 0x74, 0x23, 0x4, 0x47, 0xBC, 0xD4, 0x92, 0xF2, 0xF8, 0xA8, 0xC5, 0x94, 0xA0, 0x43, 0x79, 0x27, 0x16, 0x90, 0xBF, 0xC, 0x8A, 0x13, 0xDD, 0xFC, 0x1B, 0x7B, 0x96, 0x41, 0x3E, 0x77 },
			IV = new byte[] { 0xAB, 0x26, 0x64, 0xCB, 0xA1, 0xAC, 0xD7, 0xA3, 0xC5, 0x7E, 0xE5, 0x27 },
			AAD = new byte[] { 0x6E, 0x27, 0x41, 0x4F },
			PlainText = new byte[] { 0x82, 0x83, 0xA6, 0xF9, 0x3B, 0x73, 0xBD, 0x39, 0x2B, 0xD5, 0x41, 0xF0, 0x7E, 0xB4, 0x61, 0xA0 },
			CipherText = new byte[] { 0x62, 0xB3, 0xC9, 0x62, 0x84, 0xEE, 0x7C, 0x7C, 0xF3, 0x85, 0x42, 0x76, 0x47, 0xE4, 0xF2, 0xD1 },
			Tag = new byte[] { 0xE8, 0x2F, 0x67, 0x8A, 0x38, 0xCC, 0x2, 0x1A, 0x3, 0xC8, 0x3F, 0xB7, 0x94, 0xAF, 0x1, 0xB0 },
		},
		new TestVectorAE
		{
			Key = new byte[] { 0x80, 0xBB, 0x66, 0x7E, 0x5F, 0xF0, 0x95, 0xC9, 0x31, 0x9F, 0x57, 0x5B, 0x38, 0x93, 0x97, 0x7E, 0x65, 0x8C, 0x6C, 0x0, 0x1C, 0xEE, 0xF8, 0x8A, 0x37, 0xB7, 0x90, 0x2D, 0x4D, 0xB3, 0x1C, 0x3E },
			IV = new byte[] { 0x34, 0xF3, 0xC1, 0x64, 0xC4, 0x7B, 0xBE, 0xEF, 0xDE, 0x3B, 0x94, 0x6B },
			AAD = new byte[] { 0xAD, 0x41, 0x6A, 0x75, 0x2C, 0x2C, 0xAF, 0xCE },
			PlainText = new byte[] { 0xE9, 0xE4, 0x1, 0xAE, 0x8, 0x88, 0x4E, 0x5B, 0x8A, 0xA8, 0x39, 0xF9, 0xD0, 0xB5, 0xBF, 0xA4, 0x5A, 0xB5, 0x1A, 0xBC, 0xD5, 0x3B, 0xE5, 0x80 },
			CipherText = new byte[] { 0x19, 0x7D, 0x56, 0x6E, 0xEA, 0x9B, 0x2C, 0xFB, 0xA0, 0x9F, 0x5B, 0xDC, 0xBD, 0x89, 0xBE, 0x5F, 0x92, 0x37, 0x0, 0x63, 0x3, 0xD0, 0x91, 0x9F },
			Tag = new byte[] { 0xC6, 0x32, 0x6A, 0xCB, 0x37, 0x86, 0xF8, 0x18, 0x3D, 0xF0, 0x1B, 0xAF, 0x45, 0x35, 0x26, 0xD8 },
		},
		new TestVectorAE
		{
			Key = new byte[] { 0x55, 0x97, 0x82, 0xDF, 0xDF, 0x4A, 0xB9, 0x8A, 0x2A, 0xBD, 0xA1, 0x4E, 0xA6, 0x32, 0xC4, 0xA1, 0xBE, 0xFA, 0x7E, 0x7B, 0x5A, 0xE8, 0xA6, 0x67, 0xA9, 0x7, 0x70, 0xD9, 0x1D, 0x88, 0x92, 0xC2 },
			IV = new byte[] { 0xB7, 0xFE, 0xA6, 0x62, 0x84, 0xCE, 0xD0, 0x5D, 0x68, 0xE0, 0xA0, 0x12 },
			AAD = new byte[] { 0x1A, 0x9B, 0x3A, 0x28, 0x70, 0x11, 0xB4, 0x69, 0xDD, 0x76, 0xBF, 0xD4 },
			PlainText = new byte[] { 0x38, 0xD7, 0x8A, 0xED, 0xBE, 0x8, 0xE3, 0x58, 0x5B, 0xDF, 0xA0, 0xA6, 0x25, 0x9F, 0x3D, 0x6F, 0x29, 0xB7, 0xC6, 0xA1, 0x6B, 0xAD, 0xF7, 0x17, 0x28, 0x55, 0xA3, 0x6E, 0x5E, 0x46, 0x52, 0x14 },
			CipherText = new byte[] { 0xC3, 0x68, 0xB2, 0xE5, 0xAC, 0x83, 0x10, 0x27, 0xCA, 0xD2, 0xAE, 0xA7, 0xC3, 0xA7, 0x88, 0xBF, 0x41, 0x12, 0x17, 0x60, 0x2E, 0xD5, 0xC9, 0x79, 0x7D, 0x4C, 0xC5, 0xEA, 0x34, 0xB4, 0xB6, 0x3C },
			Tag = new byte[] { 0xD5, 0xAB, 0xBF, 0xE, 0x26, 0xAF, 0xD3, 0x71, 0x7E, 0x8E, 0xEC, 0xFC, 0xD7, 0xB5, 0xF8, 0x46 },
		},
		new TestVectorAE
		{
			Key = new byte[] { 0x8F, 0x8E, 0x2E, 0xA1, 0xC1, 0xA5, 0x20, 0x6B, 0xC9, 0xA0, 0x36, 0x54, 0xC9, 0xA7, 0x48, 0xE7, 0xF8, 0xA9, 0xDB, 0xA4, 0xA1, 0x4E, 0x45, 0x97, 0x6F, 0x2E, 0x12, 0x30, 0xE7, 0x40, 0xA7, 0x4E },
			IV = new byte[] { 0x36, 0x95, 0x25, 0x9D, 0x2D, 0x46, 0x90, 0x22, 0x8A, 0xAB, 0x31, 0x14 },
			AAD = new byte[] { 0x79, 0xCD, 0x38, 0xEE, 0x5A, 0x1E, 0xA6, 0xC2, 0xC5, 0x68, 0x6A, 0xD0, 0xCB, 0x4, 0xCA, 0x57 },
			PlainText = new byte[] { 0x7F, 0x4C, 0x5, 0xAB, 0xB6, 0x52, 0x37, 0x88, 0xA, 0xD3, 0x7C, 0xA6, 0x25, 0x66, 0x58, 0x8D, 0x3C, 0xF9, 0x57, 0xAB, 0xD9, 0x7E, 0xF0, 0xA4, 0xDE, 0xE2, 0xAF, 0xE9, 0xC7, 0x3F, 0x94, 0x8C, 0x2F, 0x52, 0xB6, 0x53, 0x95, 0x2F, 0x2B, 0xB },
			CipherText = new byte[] { 0x14, 0x8F, 0x85, 0x60, 0x5F, 0xEA, 0x6A, 0x6D, 0xF, 0xE3, 0xD3, 0xF, 0x7B, 0x15, 0x12, 0x67, 0xDC, 0x94, 0xAC, 0xBA, 0xB2, 0x2C, 0x9B, 0xC4, 0x1F, 0xB1, 0x2F, 0x34, 0x9A, 0x75, 0x7B, 0x17, 0x99, 0xE5, 0xE2, 0x20, 0xE8, 0xB4, 0xFE, 0xA3 },
			Tag = new byte[] { 0x7E, 0xB0, 0xB8, 0xDF, 0x77, 0x2D, 0x55, 0x5B, 0xA7, 0xCE, 0x72, 0xF0, 0xC4, 0xE8, 0x6A, 0xEB },
		},
		new TestVectorAE
		{
			Key = new byte[] { 0x61, 0x33, 0x2C, 0x1E, 0x22, 0x3B, 0x75, 0x68, 0x1A, 0x13, 0xEB, 0x47, 0xFC, 0x32, 0x81, 0xF1, 0xF4, 0xA4, 0x5C, 0xB, 0xE8, 0x47, 0x36, 0x77, 0xFE, 0x3A, 0x0, 0x6D, 0x5E, 0x93, 0xCB, 0xCD },
			IV = new byte[] { 0x39, 0x50, 0xE8, 0xB3, 0xA, 0x91, 0x4E, 0x4C, 0x1B, 0x4, 0x3D, 0xE9 },
			AAD = new byte[] { 0x4B, 0xA0, 0x21, 0x52, 0x10, 0x96, 0xB9, 0x5D, 0xB8, 0x84, 0x78, 0xBF, 0xF4, 0xEC, 0x2C, 0x21, 0x96, 0xE6, 0xDD, 0x77 },
			PlainText = new byte[] { 0x9A, 0x11, 0xF8, 0x8D, 0x63, 0xCF, 0xAD, 0xE2, 0x48, 0xB2, 0x95, 0xBA, 0x4F, 0x31, 0x98, 0x6F, 0x38, 0xA0, 0xE, 0x8, 0xBE, 0x5F, 0x24, 0xC, 0x19, 0x51, 0x81, 0x99, 0xC7, 0x8F, 0x2A, 0xB0, 0x8C, 0x5E, 0xA3, 0xD3, 0xB8, 0x5F, 0x58, 0xD2, 0xA8, 0x6, 0x3B, 0xE2, 0x8F, 0x4D, 0xAD, 0xEF },
			CipherText = new byte[] { 0xD3, 0x22, 0x97, 0xC8, 0x8D, 0xB5, 0x88, 0x1E, 0xF3, 0x6F, 0x7E, 0x11, 0x6D, 0x87, 0x8B, 0xA3, 0xF6, 0xB1, 0xB9, 0xA7, 0x83, 0x90, 0xD7, 0x9E, 0x1, 0xCB, 0x2E, 0xC8, 0x2C, 0xAC, 0x54, 0xDC, 0x5E, 0x5F, 0x81, 0x19, 0x9C, 0x80, 0x1F, 0x6, 0x37, 0xCB, 0x6, 0x9B, 0x8D, 0x3A, 0x0, 0x16 },
			Tag = new byte[] { 0xB4, 0x6F, 0xA6, 0x1D, 0x89, 0xFE, 0xD3, 0xFA, 0xE9, 0xAD, 0x22, 0x5B, 0xC2, 0xB7, 0x44, 0x15 },
		},
		new TestVectorAE
		{
			Key = new byte[] { 0x77, 0xAA, 0xA2, 0x33, 0x82, 0x3E, 0x0, 0x8, 0x76, 0x4F, 0x49, 0xFA, 0x78, 0xF8, 0x7A, 0x21, 0x18, 0x1F, 0x33, 0xAE, 0x8E, 0xA8, 0x17, 0xC3, 0x43, 0xE8, 0x76, 0x88, 0x94, 0x5D, 0x2A, 0x7B },
			IV = new byte[] { 0xD2, 0x9C, 0xBE, 0x7, 0x8D, 0x8A, 0xD6, 0x59, 0x12, 0xCF, 0xCA, 0x6F },
			AAD = new byte[] { 0x32, 0x88, 0x95, 0x71, 0x45, 0x3C, 0xEE, 0x45, 0x6F, 0x12, 0xB4, 0x5E, 0x22, 0x41, 0x8F, 0xD4, 0xE4, 0xC7, 0xD5, 0xBA, 0x53, 0x5E, 0xAA, 0xAC },
			PlainText = new byte[] { 0x66, 0xAC, 0x6C, 0xA7, 0xF5, 0xBA, 0x4E, 0x1D, 0x7C, 0xA7, 0x42, 0x49, 0x1C, 0x9E, 0x1D, 0xC1, 0xE2, 0x5, 0xF5, 0x4A, 0x4C, 0xF7, 0xCE, 0xEF, 0x9, 0xF5, 0x76, 0x55, 0x1, 0xD8, 0xAE, 0x49, 0x95, 0xA, 0x8A, 0x9B, 0x28, 0xF6, 0x1B, 0x2F, 0xDE, 0xBD, 0x4B, 0x51, 0xA3, 0x2B, 0x7, 0x49, 0x70, 0xE9, 0xA4, 0x2F, 0xC9, 0xF4, 0x7B, 0x1 },
			CipherText = new byte[] { 0x1E, 0x98, 0xB, 0xC3, 0xD9, 0x70, 0xEC, 0x90, 0x4, 0x17, 0x7F, 0x5E, 0xE0, 0xE9, 0xBA, 0xCA, 0x2F, 0x49, 0x28, 0x36, 0x71, 0x8, 0x69, 0xE5, 0x91, 0xA2, 0xC, 0xF, 0xA4, 0x12, 0xFF, 0xAE, 0xD9, 0x5F, 0x98, 0x50, 0xCF, 0x93, 0xB4, 0xFB, 0x9F, 0x43, 0x1A, 0xD8, 0x55, 0x5F, 0x4B, 0x3A, 0xE7, 0xC8, 0x1E, 0xAE, 0x61, 0x29, 0x81, 0x1F },
			Tag = new byte[] { 0xE3, 0xEE, 0x8A, 0x8E, 0x4, 0xEE, 0x49, 0x4B, 0x2B, 0x54, 0xD7, 0xDC, 0xEA, 0xCD, 0xBA, 0xD6 },
		},
		new TestVectorAE
		{
			Key = new byte[] { 0x8B, 0x4D, 0xD7, 0xF3, 0xE4, 0xF3, 0x12, 0x6, 0xA3, 0xE, 0xFA, 0xDC, 0xB2, 0x6D, 0x79, 0xCB, 0xE0, 0x32, 0x76, 0x30, 0xE4, 0xCC, 0xF6, 0x6, 0x9F, 0x26, 0x87, 0xB7, 0xA2, 0x1E, 0xDE, 0x31 },
			IV = new byte[] { 0xF0, 0x39, 0xBF, 0x3D, 0x5, 0x95, 0x1D, 0xC, 0x17, 0x1F, 0x83, 0x13 },
			AAD = new byte[] { 0x9B, 0x5, 0xB2, 0x1A, 0xB, 0x6F, 0x8C, 0x8F, 0xE7, 0x6A, 0x30, 0x1F, 0xFA, 0x24, 0x67, 0x88, 0xDE, 0x5F, 0x3F, 0xA8, 0xC2, 0x20, 0xF5, 0xDA, 0x6B, 0xA7, 0x18, 0x83 },
			PlainText = new byte[] { 0x79, 0xA4, 0xEC, 0xE8, 0xEC, 0xA8, 0xD, 0x43, 0xE6, 0xDA, 0x54, 0x15, 0xB2, 0xEB, 0xFE, 0xBD, 0x6, 0x4C, 0xB, 0x4A, 0x85, 0x9E, 0xE5, 0x8E, 0x88, 0x6A, 0x42, 0x73, 0x1E, 0x12, 0x4B, 0x93, 0x52, 0x7, 0xFE, 0x2, 0xAC, 0x3D, 0xD1, 0x6E, 0xFA, 0xDC, 0x98, 0x6B, 0x4F, 0x39, 0xA8, 0x8, 0x4D, 0x4, 0x3D, 0xA6, 0xA0, 0xC3, 0x19, 0xA, 0xCB, 0x7E, 0x6E, 0xB0, 0x27, 0xBC, 0xFE, 0x62 },
			CipherText = new byte[] { 0x5D, 0xF5, 0x44, 0x31, 0x60, 0x34, 0x7A, 0xEF, 0x5E, 0x9E, 0xD8, 0x3F, 0x6A, 0x8C, 0xF4, 0xA5, 0x8E, 0x41, 0xA8, 0x52, 0x62, 0x4E, 0xDB, 0x7D, 0x3B, 0x36, 0x9A, 0x29, 0x96, 0x77, 0xEE, 0x8C, 0x5D, 0xE9, 0x80, 0xF5, 0x4E, 0x4E, 0x3F, 0x63, 0x8C, 0xDB, 0x9E, 0xA4, 0x11, 0x3D, 0x24, 0xA4, 0xC6, 0xD8, 0x92, 0x4, 0xEA, 0x3E, 0xC7, 0x35, 0xCF, 0xFE, 0x9F, 0xA0, 0xED, 0x9D, 0x65, 0x9C },
			Tag = new byte[] { 0x39, 0xBB, 0xF3, 0xE2, 0x1D, 0x4D, 0x9C, 0xDD, 0x3B, 0x3F, 0x5D, 0x38, 0xFA, 0xD5, 0x6F, 0x43 },
		},
		new TestVectorAE
		{
			Key = new byte[] { 0x28, 0xAF, 0x81, 0xA, 0xE3, 0xAC, 0x47, 0xC5, 0xCD, 0xDE, 0x1E, 0x38, 0xC6, 0x3A, 0x76, 0x27, 0x56, 0xB5, 0xC3, 0x43, 0x36, 0xB5, 0x23, 0x44, 0x12, 0xC6, 0x41, 0x38, 0x8D, 0x8F, 0x79, 0x1B },
			IV = new byte[] { 0x86, 0x4, 0x3D, 0xA7, 0x99, 0xDD, 0xB7, 0x6D, 0xE9, 0x46, 0xAF, 0x25 },
			AAD = new byte[] { 0x1D, 0xD8, 0xB0, 0x43, 0xA9, 0xC8, 0x66, 0xCB, 0x4F, 0x5E, 0x4B, 0x65, 0xE5, 0xA9, 0x83, 0xB4, 0x58, 0x72, 0x3, 0xE4, 0xF1, 0x6E, 0xF9, 0x82, 0xD7, 0xB7, 0x68, 0x91, 0xD6, 0x7D, 0x13, 0xDA },
			PlainText = new byte[] { 0x5E, 0xF9, 0x9D, 0x83, 0xCF, 0xA3, 0xEE, 0xE3, 0xF3, 0xA5, 0xD9, 0x95, 0xCC, 0x8F, 0xFB, 0xCB, 0x91, 0x4C, 0xE6, 0xE5, 0xF3, 0x55, 0x7F, 0x43, 0xA4, 0x24, 0xA4, 0x57, 0x1A, 0xEC, 0x12, 0xFE, 0x91, 0x87, 0x86, 0x10, 0x4F, 0xB0, 0x23, 0x35, 0x2B, 0x72, 0x14, 0xFB, 0x50, 0xE0, 0x72, 0x26, 0x3F, 0x7, 0x68, 0x48, 0x13, 0x21, 0x95, 0x91, 0x9, 0xC, 0xB1, 0xE7, 0x3F, 0xFA, 0x74, 0x39, 0xBC, 0x69, 0xB3, 0x11, 0xB1, 0x56, 0xCD, 0x69 },
			CipherText = new byte[] { 0x6A, 0x7E, 0x5C, 0x94, 0x40, 0x7B, 0x46, 0xD, 0x1D, 0xE0, 0x1D, 0x59, 0x35, 0x84, 0xD8, 0xA5, 0xF0, 0xE2, 0x8D, 0x96, 0x7E, 0x5E, 0xD9, 0xBD, 0xBD, 0xA8, 0x3B, 0x95, 0x90, 0x0, 0x86, 0x98, 0xD0, 0x78, 0xB6, 0x5D, 0x8B, 0xA8, 0x75, 0xED, 0x4A, 0x7B, 0xB7, 0xE6, 0x5A, 0xD0, 0x53, 0x67, 0xE8, 0x92, 0xB4, 0x4C, 0x17, 0x8B, 0x36, 0xC3, 0x6C, 0xA6, 0x42, 0x24, 0x93, 0x2E, 0x10, 0xD4, 0x81, 0xF1, 0x8B, 0xF1, 0xB5, 0xEF, 0xF0, 0x57 },
			Tag = new byte[] { 0x87, 0x84, 0x79, 0xF6, 0x50, 0x8A, 0x5, 0x8F, 0x2E, 0x6C, 0x8C, 0x56, 0x12, 0x36, 0x5E, 0x7B },
		},
		new TestVectorAE
		{
			Key = new byte[] { 0xFE, 0xAD, 0x45, 0xC0, 0xF8, 0x5, 0xB0, 0x6D, 0x6A, 0x8A, 0xD2, 0x32, 0xDA, 0x3A, 0x4, 0x14, 0xD, 0x55, 0xD8, 0x6A, 0xCD, 0xE, 0x0, 0x35, 0xE9, 0x87, 0x6C, 0xAF, 0x7E, 0xFC, 0xB3, 0x27 },
			IV = new byte[] { 0x75, 0x3F, 0xB4, 0xB, 0x4D, 0x64, 0x7D, 0x47, 0x1A, 0xBD, 0x6C, 0xCD },
			AAD = new byte[] { 0xCE, 0x9, 0x93, 0x75, 0xB8, 0xE8, 0x61, 0x8, 0x4A, 0x92, 0x7F, 0x97, 0x1F, 0xC9, 0xF9, 0x12, 0x3D, 0xC, 0x9E, 0x16, 0x97, 0xE, 0xA9, 0x5, 0xD6, 0x66, 0xA1, 0x5C, 0x58, 0x88, 0x7D, 0x41, 0x79, 0xF4, 0xC, 0x5C },
			PlainText = new byte[] { 0x12, 0xAD, 0x94, 0xE8, 0x40, 0xED, 0xBA, 0xA, 0x67, 0x17, 0xA1, 0x61, 0x76, 0xED, 0x56, 0xDE, 0x6C, 0x3, 0x6C, 0xD6, 0x1B, 0x73, 0xE3, 0x55, 0x2B, 0xD3, 0x4F, 0xB6, 0x63, 0x64, 0x35, 0x70, 0x24, 0x8C, 0xB9, 0x33, 0x36, 0x87, 0x76, 0x34, 0xC8, 0x59, 0xAE, 0xC5, 0xB1, 0x7, 0xA3, 0x28, 0xFC, 0x6, 0x44, 0xA5, 0xA4, 0xF4, 0xC, 0xDA, 0x9C, 0x86, 0x28, 0x53, 0x12, 0xC2, 0xD8, 0x5A, 0xF5, 0x6E, 0x17, 0x10, 0xB5, 0xC8, 0x7E, 0xBD, 0x4A, 0x77, 0x66, 0x65, 0x77, 0xC2, 0x4D, 0x9C },
			CipherText = new byte[] { 0x48, 0xC5, 0xBA, 0x53, 0x2F, 0x9B, 0x82, 0x4D, 0xC9, 0x58, 0xCE, 0x0, 0x8D, 0x4E, 0x43, 0xDE, 0x71, 0x14, 0x92, 0x9D, 0x72, 0x39, 0xEE, 0x8B, 0xD7, 0xF4, 0x2C, 0x20, 0xB5, 0x39, 0x7F, 0x18, 0xB8, 0x82, 0x10, 0x22, 0xCA, 0xEF, 0x3C, 0xC3, 0xD0, 0x15, 0x93, 0x19, 0x1C, 0xDE, 0x2C, 0x3D, 0x76, 0xF3, 0x15, 0xAE, 0xFA, 0xF2, 0x61, 0xEC, 0x5B, 0x4A, 0x3, 0xE, 0x8C, 0x2F, 0xDC, 0x9E, 0xE9, 0x51, 0x95, 0x9C, 0x9C, 0x48, 0x66, 0x29, 0xB5, 0x82, 0xEC, 0xB0, 0x81, 0x40, 0x54, 0xE6 },
			Tag = new byte[] { 0x41, 0xFE, 0xD2, 0x2B, 0x59, 0x3A, 0x9, 0x7A, 0xF8, 0xE8, 0xAB, 0xB1, 0xF4, 0x61, 0xC0, 0xAB },
		}
	};
		private readonly TestVectorAE[] lea192GcmTestVectors =
		{
		new TestVectorAE
		{
			Key = new byte[] { 0x16, 0x3D, 0x97, 0x4E, 0x46, 0xB3, 0x5E, 0xB7, 0xD, 0x9C, 0x30, 0xDD, 0x4, 0xB6, 0x1F, 0x9B, 0x6F, 0xCB, 0x1E, 0x26, 0x7B, 0xE3, 0x65, 0xE4 },
			IV = new byte[] { 0xC4, 0xB8, 0x16, 0x82, 0x1A, 0x6A, 0x20, 0x41, 0x85, 0x74, 0x63, 0x90 },
			AAD = Array.Empty<byte>(),
			PlainText = new byte[] { 0x86, 0xDF, 0x16, 0xE0, 0xA0, 0xB5, 0x66, 0xB4 },
			CipherText = new byte[] { 0xD8, 0x4B, 0x14, 0x9B, 0xA4, 0xE7, 0x78, 0xF8 },
			Tag = new byte[] { 0x2F, 0x5, 0x27, 0xA0, 0x23, 0x32, 0xC1, 0xF5, 0xDD, 0xCF, 0x9, 0xD5, 0x91, 0xE6, 0xE5, 0xB7 },
		},
		new TestVectorAE
		{
			Key = new byte[] { 0x17, 0xB9, 0x60, 0xF7, 0xF8, 0x55, 0x11, 0x93, 0x59, 0xD4, 0xEC, 0x3F, 0xE2, 0xD1, 0x6C, 0x98, 0x8D, 0xEF, 0xFA, 0x74, 0xA9, 0xCB, 0x10, 0x73 },
			IV = new byte[] { 0x25, 0x2F, 0x9F, 0xD7, 0xB, 0x89, 0xAE, 0x95, 0x4B, 0x9B, 0x44, 0xAB },
			AAD = new byte[] { 0xBA, 0x73, 0x89, 0xB5 },
			PlainText = new byte[] { 0x5A, 0x9E, 0x28, 0xBB, 0xDC, 0x99, 0xF7, 0x4E, 0x63, 0x88, 0xDA, 0xD3, 0x8B, 0x2A, 0xE5, 0x1E },
			CipherText = new byte[] { 0x61, 0x20, 0x80, 0x98, 0xA2, 0x0, 0xC2, 0x8C, 0x38, 0x57, 0x96, 0x20, 0x7B, 0x28, 0x2, 0xDD },
			Tag = new byte[] { 0xCB, 0xD9, 0xEB, 0x59, 0x79, 0x72, 0xCA, 0x4D, 0x82, 0xAF, 0x5E, 0x73, 0x3C, 0x4A, 0x73, 0xF2 },
		},
		new TestVectorAE
		{
			Key = new byte[] { 0xAA, 0xD, 0xC0, 0x43, 0x56, 0xFE, 0x50, 0xA8, 0x27, 0xC4, 0x93, 0x5F, 0xBD, 0xE, 0xCC, 0xB3, 0x5, 0xA9, 0xE1, 0x1, 0xB9, 0xF6, 0x1, 0xCA },
			IV = new byte[] { 0x26, 0x9C, 0x89, 0x5, 0x32, 0x7A, 0x29, 0xBA, 0x9D, 0xE0, 0x43, 0xCC },
			AAD = new byte[] { 0x87, 0xFE, 0x93, 0x17, 0x33, 0x9C, 0x15, 0xBA },
			PlainText = new byte[] { 0xA, 0xC, 0x23, 0xB6, 0xE3, 0x5D, 0xDE, 0xB9, 0x81, 0xD2, 0xD0, 0x34, 0x5D, 0x92, 0xD0, 0xB3, 0xA6, 0xA1, 0x25, 0x6E, 0x87, 0xF, 0xEE, 0x1B },
			CipherText = new byte[] { 0x1F, 0xBE, 0x14, 0x5, 0x46, 0xAE, 0x8C, 0x9B, 0x1, 0x8E, 0xCC, 0x66, 0x43, 0xBA, 0xD5, 0x98, 0xA0, 0x65, 0xEE, 0xC4, 0x46, 0xAD, 0x6E, 0xA7 },
			Tag = new byte[] { 0xED, 0x75, 0xE2, 0x92, 0x3F, 0xA, 0x86, 0xC5, 0x3E, 0xA5, 0x73, 0x97, 0xD1, 0x71, 0x76, 0xB4 },
		},
		new TestVectorAE
		{
			Key = new byte[] { 0xA8, 0x70, 0xC1, 0x7, 0xF7, 0x8C, 0x92, 0x65, 0xA8, 0x57, 0xD6, 0xE6, 0x7A, 0x23, 0xE9, 0x8A, 0x3D, 0x14, 0xAD, 0xB5, 0x91, 0xD4, 0x75, 0x85 },
			IV = new byte[] { 0xF0, 0x89, 0x21, 0x63, 0xEF, 0x4, 0x8A, 0xD8, 0xC0, 0x3B, 0x20, 0xA2 },
			AAD = new byte[] { 0xFC, 0xFA, 0xD1, 0x8, 0x9F, 0xD5, 0x2D, 0x6A, 0x55, 0x61, 0xC8, 0x1C },
			PlainText = new byte[] { 0xF4, 0xA4, 0xE0, 0x75, 0x49, 0xC9, 0x40, 0x22, 0x17, 0x18, 0x64, 0xC0, 0x5D, 0x26, 0xDE, 0xAB, 0xD8, 0x49, 0xF9, 0x10, 0xC9, 0x4F, 0x9B, 0x4A, 0xF8, 0x70, 0x70, 0x6B, 0xF9, 0x80, 0x44, 0x18 },
			CipherText = new byte[] { 0xEB, 0xA, 0xD2, 0x9B, 0xBD, 0xF1, 0xFE, 0x5C, 0xB5, 0x7E, 0x82, 0xFE, 0xEF, 0x98, 0xCD, 0x20, 0xB8, 0x26, 0x46, 0x1F, 0xA7, 0xC4, 0xB1, 0xBA, 0x4, 0x27, 0xBC, 0xE8, 0x28, 0x8B, 0xE2, 0x9C },
			Tag = new byte[] { 0x68, 0x49, 0x11, 0xA, 0x5B, 0x8D, 0x2E, 0x55, 0xB3, 0x73, 0xF9, 0x78, 0x4B, 0xD4, 0x34, 0x5F },
		},
		new TestVectorAE
		{
			Key = new byte[] { 0xAE, 0x6A, 0xB1, 0x9B, 0xD, 0xD9, 0xFB, 0x33, 0x23, 0x38, 0x38, 0xAE, 0x59, 0xDD, 0xB4, 0x3A, 0xF7, 0x81, 0x93, 0x79, 0xE6, 0xF4, 0xB8, 0xD3 },
			IV = new byte[] { 0xBA, 0xE, 0xE6, 0x10, 0xF0, 0xE9, 0x25, 0x26, 0x73, 0x4D, 0x68, 0x4F },
			AAD = new byte[] { 0x65, 0x6C, 0x6C, 0x5F, 0x1D, 0xD0, 0xE3, 0x55, 0x6C, 0x91, 0xD2, 0x30, 0x25, 0xCB, 0x38, 0x4 },
			PlainText = new byte[] { 0xDC, 0x4F, 0xF0, 0x4C, 0xED, 0x99, 0xD8, 0x82, 0xC1, 0x3, 0x33, 0xED, 0x4C, 0x38, 0x4C, 0xBB, 0xDA, 0xEA, 0x5D, 0x4F, 0xC, 0xC8, 0xAE, 0xDC, 0x1D, 0xAA, 0x1, 0x33, 0x6A, 0xF, 0x2E, 0xD9, 0x32, 0xCA, 0xB, 0x5D, 0x9A, 0xF8, 0x8E, 0xE8 },
			CipherText = new byte[] { 0xCC, 0x7A, 0x78, 0x77, 0x69, 0x1A, 0x22, 0x59, 0xFE, 0xE4, 0x3C, 0xBE, 0x71, 0x5A, 0xE5, 0xA8, 0xFA, 0x15, 0x6F, 0x0, 0x28, 0xF2, 0x28, 0xD7, 0x4D, 0x5D, 0x9, 0x96, 0x17, 0x19, 0x9, 0xE, 0xEA, 0x18, 0xE5, 0x95, 0xC9, 0xA5, 0xEC, 0xF },
			Tag = new byte[] { 0x7E, 0x37, 0x53, 0x77, 0xD, 0x99, 0xAD, 0xC9, 0xAD, 0x38, 0xA6, 0x89, 0x91, 0xCA, 0x6E, 0x25 },
		},
		new TestVectorAE
		{
			Key = new byte[] { 0xF4, 0x35, 0x36, 0x16, 0x40, 0x10, 0xA6, 0x4, 0xF3, 0xBC, 0x94, 0xAB, 0xF8, 0x8, 0xE1, 0x6C, 0xF5, 0x90, 0x9, 0xEB, 0xCF, 0x35, 0xBE, 0x21 },
			IV = new byte[] { 0x70, 0xCC, 0xD0, 0xAD, 0xC8, 0x13, 0x50, 0x6B, 0x11, 0xE9, 0xF5, 0x48 },
			AAD = new byte[] { 0x58, 0xBE, 0xBE, 0x55, 0x39, 0x48, 0xD9, 0x1A, 0xEA, 0x77, 0xC4, 0x2C, 0x7B, 0xAC, 0xB2, 0x2, 0x5B, 0x26, 0x30, 0x4 },
			PlainText = new byte[] { 0x1F, 0xBC, 0x8, 0xE5, 0x7F, 0xD2, 0x66, 0x33, 0xA1, 0x86, 0x39, 0x2C, 0x9B, 0x2A, 0xDC, 0xD4, 0x33, 0xF7, 0xF6, 0x44, 0xE8, 0xFF, 0x9F, 0x45, 0x37, 0x64, 0xC4, 0x1D, 0x17, 0xC8, 0xCC, 0xA9, 0xC6, 0x4, 0x7B, 0xAC, 0xC7, 0x1D, 0x17, 0x67, 0x2C, 0x74, 0xCA, 0x6A, 0x11, 0x3C, 0x48, 0xA9 },
			CipherText = new byte[] { 0x5F, 0xCF, 0x29, 0xA6, 0x15, 0x8D, 0xF1, 0xFB, 0xFF, 0xED, 0xD2, 0x97, 0x91, 0x8B, 0x7, 0x29, 0xBF, 0x71, 0xCA, 0x6, 0xC, 0x86, 0x9E, 0x1C, 0x52, 0x13, 0x58, 0xC4, 0xD2, 0xA8, 0x56, 0xF3, 0xFC, 0xC5, 0x8, 0xB5, 0x4, 0x26, 0x4E, 0xAE, 0x17, 0x8D, 0xC9, 0xE5, 0xD1, 0x52, 0x59, 0xAF },
			Tag = new byte[] { 0xFA, 0x4A, 0x63, 0xF7, 0xE, 0xE, 0x83, 0x58, 0x8F, 0xF8, 0xEF, 0xDB, 0x6E, 0x9D, 0xB0, 0xE2 },
		},
		new TestVectorAE
		{
			Key = new byte[] { 0x69, 0x31, 0xB1, 0xD2, 0xFD, 0x87, 0xB9, 0xDE, 0xB1, 0x21, 0x6, 0xE7, 0x9, 0x4, 0xDA, 0x39, 0xED, 0xB, 0xF1, 0xAB, 0xA9, 0xDC, 0xAD, 0x30 },
			IV = new byte[] { 0x38, 0x1A, 0x70, 0xA9, 0xC2, 0xDD, 0x49, 0xFD, 0x63, 0x5F, 0xD4, 0x6C },
			AAD = new byte[] { 0x2E, 0xF8, 0x5B, 0x20, 0x72, 0x4C, 0x41, 0x5, 0x3B, 0xC3, 0xA1, 0xDB, 0x1C, 0x39, 0x32, 0x8A, 0x2C, 0xF7, 0x6E, 0xB5, 0x4F, 0xE2, 0xF3, 0x91 },
			PlainText = new byte[] { 0xB7, 0xF3, 0x29, 0xF8, 0xAA, 0xE7, 0x25, 0xB9, 0x85, 0x38, 0xCD, 0x33, 0x1, 0x4B, 0x3D, 0x22, 0xB5, 0xEB, 0x6A, 0xB8, 0xDC, 0xF6, 0x5, 0xF0, 0x59, 0x56, 0xA3, 0x1F, 0xF8, 0x33, 0x1A, 0xCB, 0xF7, 0x67, 0x2E, 0xC1, 0xC5, 0x33, 0x6D, 0x62, 0x9, 0x30, 0x57, 0x3E, 0xE6, 0x88, 0xC2, 0xE4, 0x7D, 0x65, 0x7D, 0xF7, 0xB4, 0xAA, 0x34, 0x85 },
			CipherText = new byte[] { 0x90, 0x5B, 0x54, 0x95, 0x60, 0x15, 0x65, 0x7D, 0x39, 0xFE, 0x8D, 0xD3, 0x1D, 0x23, 0x0, 0xB, 0xA1, 0xA2, 0x9E, 0x9E, 0x19, 0x6D, 0x75, 0x3A, 0xE6, 0x65, 0xB5, 0x4F, 0xBE, 0xB4, 0x22, 0xC3, 0x78, 0xC5, 0x66, 0x5C, 0xB1, 0x44, 0xC3, 0xFF, 0xAE, 0x16, 0xB7, 0x83, 0xEE, 0x72, 0x13, 0x77, 0x35, 0x3B, 0xEF, 0xFF, 0x32, 0x94, 0x80, 0x7A },
			Tag = new byte[] { 0x50, 0x13, 0x6C, 0xE1, 0x6B, 0x4A, 0x37, 0x4B, 0x42, 0xCB, 0x67, 0x65, 0xD6, 0x6B, 0xCD, 0xB3 },
		},
		new TestVectorAE
		{
			Key = new byte[] { 0x36, 0xE2, 0x93, 0x95, 0xBC, 0x75, 0xAE, 0xFF, 0x8A, 0x22, 0xA1, 0x81, 0x3C, 0xA9, 0x73, 0xE, 0xC2, 0xC8, 0x41, 0x69, 0xAA, 0x69, 0x98, 0xF3 },
			IV = new byte[] { 0x9D, 0x1A, 0x22, 0xC2, 0x2D, 0xBD, 0x85, 0xF2, 0xCE, 0x81, 0x88, 0x40 },
			AAD = new byte[] { 0x21, 0x11, 0x78, 0xD4, 0x79, 0xB, 0xCA, 0x5F, 0x98, 0xB3, 0x1, 0x66, 0xB9, 0xE9, 0xD2, 0xDE, 0xD4, 0x58, 0x86, 0xF8, 0x1D, 0xF1, 0xA1, 0x3E, 0xCD, 0x98, 0xC1, 0xDF },
			PlainText = new byte[] { 0x26, 0x1C, 0xC8, 0x47, 0xB1, 0x6D, 0x3D, 0xF3, 0xCB, 0x9, 0xF0, 0x84, 0x5F, 0xB8, 0xDD, 0x12, 0xF5, 0x79, 0x53, 0xC2, 0xEC, 0xBC, 0x56, 0x9B, 0x3, 0xD1, 0x41, 0x95, 0x1F, 0xA1, 0xB0, 0xF3, 0x48, 0x9C, 0x9C, 0xD3, 0xF4, 0xF2, 0xC8, 0x4, 0x88, 0x6, 0x5C, 0x17, 0x1C, 0x20, 0xD0, 0xB9, 0x7E, 0x61, 0xC, 0x40, 0x7A, 0xFD, 0xCE, 0x83, 0x59, 0xA4, 0x4B, 0xEF, 0xA8, 0x41, 0x19, 0xD7 },
			CipherText = new byte[] { 0x26, 0x30, 0x30, 0xA2, 0xE, 0xA6, 0x53, 0x41, 0x44, 0x60, 0x4A, 0xA2, 0x83, 0xDF, 0x2D, 0xE, 0x63, 0x64, 0x5D, 0xE5, 0xC, 0x3D, 0x8, 0xE, 0x4D, 0x1C, 0x36, 0xAB, 0x4A, 0xDB, 0x31, 0xFB, 0x2D, 0xAB, 0x2B, 0xB9, 0xB4, 0xFA, 0xF0, 0x1A, 0xA3, 0x12, 0xA9, 0xF0, 0x8A, 0x9E, 0x92, 0xE0, 0xB, 0x95, 0x7A, 0x67, 0xC2, 0x9B, 0x4C, 0x8E, 0x93, 0xA6, 0xD3, 0x90, 0xA2, 0x26, 0x8B, 0xEE },
			Tag = new byte[] { 0xB4, 0x82, 0x7C, 0x1A, 0x7B, 0x99, 0xB0, 0x95, 0x6F, 0xB, 0x7, 0xB4, 0xF1, 0x1F, 0x98, 0x39 },
		},
		new TestVectorAE
		{
			Key = new byte[] { 0x3B, 0xE7, 0x4C, 0xC, 0x71, 0x8, 0xE0, 0xAE, 0xB8, 0xE9, 0x57, 0x41, 0x54, 0x52, 0xA2, 0x3, 0x5D, 0x8A, 0x45, 0x7D, 0x7, 0x83, 0xB7, 0x59 },
			IV = new byte[] { 0x27, 0x51, 0x7, 0x73, 0xF2, 0xE0, 0xC5, 0x33, 0x7, 0xE7, 0x20, 0x19 },
			AAD = new byte[] { 0xB0, 0x18, 0x4C, 0x99, 0x64, 0x9A, 0x27, 0x2A, 0x91, 0xB8, 0x1B, 0x9A, 0x99, 0xDB, 0x46, 0xA4, 0x1A, 0xB5, 0xD8, 0xC4, 0x73, 0xC0, 0xBD, 0x4A, 0x84, 0xE7, 0x7D, 0xAE, 0xB5, 0x82, 0x60, 0x23 },
			PlainText = new byte[] { 0x39, 0x88, 0xD5, 0x6E, 0x94, 0x0, 0x14, 0xF9, 0x5A, 0xB9, 0x3, 0x23, 0x3A, 0x3B, 0x56, 0xDB, 0x3C, 0xFD, 0xFB, 0x6D, 0x47, 0xD9, 0xB5, 0x9B, 0xE6, 0xBC, 0x7, 0xF0, 0x4B, 0xA2, 0x53, 0x51, 0x95, 0xC2, 0x43, 0xD5, 0x4E, 0x5, 0x68, 0xD7, 0x38, 0xBD, 0x21, 0x49, 0x49, 0x94, 0xBF, 0x4A, 0xF4, 0xC2, 0xE6, 0xFB, 0xAA, 0x84, 0x36, 0x8F, 0xA1, 0xC9, 0x2B, 0xA2, 0xD4, 0x2E, 0x42, 0xCC, 0x4B, 0x2C, 0x5E, 0x75, 0x9C, 0x90, 0x69, 0xEB },
			CipherText = new byte[] { 0x84, 0xE1, 0x22, 0x8E, 0x1D, 0xD6, 0x26, 0xE0, 0xFC, 0xBB, 0x5E, 0x50, 0x43, 0x66, 0x4E, 0xB1, 0x2C, 0xA2, 0xB4, 0x8D, 0x2A, 0x57, 0x52, 0x1E, 0xE1, 0x90, 0x25, 0xB, 0x12, 0x1D, 0x8F, 0xCB, 0x81, 0xAE, 0xDC, 0x6, 0xC6, 0xA8, 0x4B, 0xD7, 0xA5, 0xBF, 0xBB, 0x84, 0xA9, 0x9B, 0x49, 0xA5, 0xCD, 0x8E, 0xEC, 0x3B, 0x89, 0xCE, 0x99, 0x86, 0x1F, 0xED, 0xFC, 0x8, 0x17, 0xD9, 0xE5, 0x9C, 0x8A, 0x29, 0xB, 0x7F, 0x32, 0x6C, 0x9A, 0x99 },
			Tag = new byte[] { 0x53, 0x5E, 0xCD, 0xE5, 0x6E, 0x60, 0xF3, 0x3E, 0x3A, 0x50, 0x5B, 0x39, 0xB, 0x6, 0xF4, 0xB },
		},
		new TestVectorAE
		{
			Key = new byte[] { 0xB2, 0x2C, 0x3E, 0xB1, 0xCD, 0xCD, 0xC7, 0x1B, 0xCA, 0x6D, 0x62, 0x17, 0xA5, 0xA9, 0x8A, 0x4E, 0x3C, 0x75, 0xB3, 0x6C, 0x55, 0xE, 0x75, 0xBB },
			IV = new byte[] { 0xE3, 0x30, 0xED, 0xF7, 0x85, 0x8A, 0x61, 0x5E, 0x50, 0x6F, 0x22, 0x6 },
			AAD = new byte[] { 0xCE, 0xCE, 0xB7, 0x73, 0x46, 0x64, 0x37, 0x69, 0x3E, 0x30, 0xF7, 0xFF, 0x3F, 0x27, 0x65, 0xF5, 0xD8, 0x2A, 0x34, 0x47, 0xE5, 0x35, 0xBA, 0x2, 0x12, 0xD9, 0x96, 0x57, 0x9A, 0xEC, 0x95, 0xEE, 0x55, 0x7F, 0xD7, 0x7D },
			PlainText = new byte[] { 0xF1, 0x10, 0x2D, 0x98, 0x7F, 0x1, 0xC6, 0xCB, 0x32, 0x49, 0xB, 0xE6, 0x64, 0x6A, 0xC7, 0x98, 0xDA, 0xA3, 0x8B, 0x3F, 0x49, 0x66, 0x51, 0xFF, 0x19, 0xC, 0x5F, 0x11, 0x68, 0xC8, 0x6E, 0x5E, 0x52, 0xD9, 0xC, 0x4D, 0x70, 0x4, 0x3F, 0xD9, 0x9E, 0x42, 0x6B, 0xE6, 0x2, 0xB6, 0x75, 0xD5, 0x4A, 0xE1, 0x29, 0x56, 0x34, 0x19, 0xD9, 0x7D, 0x54, 0x37, 0x47, 0x19, 0x12, 0x91, 0xC6, 0x41, 0xF2, 0x28, 0x9B, 0x2F, 0x18, 0x22, 0xA9, 0x52, 0xB, 0x7A, 0x4E, 0x9E, 0xB9, 0xF6, 0x88, 0x27 },
			CipherText = new byte[] { 0x59, 0xE6, 0x47, 0xBD, 0x51, 0xD0, 0xBB, 0x13, 0x6C, 0x2C, 0x14, 0x2D, 0x3, 0xA2, 0x34, 0x3C, 0xC3, 0x69, 0x9A, 0x1C, 0x2B, 0x82, 0xD, 0x9D, 0x37, 0xE7, 0xB, 0x80, 0x1B, 0xAC, 0x20, 0xD9, 0x80, 0xF4, 0x9F, 0x8F, 0x72, 0x3E, 0xCD, 0x9A, 0x23, 0xA2, 0x9E, 0x22, 0x49, 0xB2, 0xC6, 0xEF, 0x86, 0x5B, 0x18, 0x81, 0x43, 0x16, 0xBF, 0xCA, 0x17, 0xBC, 0xF3, 0x8, 0x51, 0xBC, 0x21, 0x4C, 0xF7, 0x3, 0xA2, 0x7B, 0x94, 0x8A, 0x26, 0x5, 0xA6, 0xB, 0xC7, 0x3E, 0x3E, 0x86, 0xD8, 0xD8 },
			Tag = new byte[] { 0x39, 0xB7, 0xDA, 0xF0, 0x2B, 0xBA, 0x69, 0xE1, 0xD0, 0x12, 0xB1, 0x39, 0x29, 0xE, 0x6B, 0x14 },
		}
	};
		private readonly TestVectorAE[] lea128GcmTestVectors =
		{
		new TestVectorAE
		{
			Key = new byte[] { 0xA4, 0x94, 0x52, 0x9D, 0x9C, 0xAC, 0x44, 0x59, 0xF0, 0x57, 0x8C, 0xDF, 0x7F, 0x87, 0xA8, 0xC9 },
			IV = new byte[] { 0x4B, 0xC3, 0x50, 0xF9, 0x7F, 0x1D, 0xA1, 0x2C, 0xB1, 0x64, 0x7B, 0xD2 },
			AAD = Array.Empty<byte>(),
			PlainText = new byte[] { 0x64, 0x9A, 0x28, 0x1E, 0xD1, 0xA8, 0x3E, 0x59 },
			CipherText = new byte[] { 0xE8, 0xEA, 0xA3, 0x5E, 0xB6, 0x2E, 0x25, 0xCB },
			Tag = new byte[] { 0x9D, 0xFE, 0x1E, 0xD1, 0xDC, 0x53, 0x3C, 0x11, 0x4F, 0x6, 0x50, 0x8B, 0x18, 0x9C, 0xC6, 0x52 },
		},
		new TestVectorAE
		{
			Key = new byte[] { 0xE7, 0x51, 0xC6, 0x7B, 0x90, 0x65, 0x9D, 0xFC, 0x36, 0x37, 0x1B, 0xE8, 0x86, 0x50, 0xF3, 0x32 },
			IV = new byte[] { 0x73, 0x65, 0x5A, 0x4A, 0x7E, 0x87, 0x6C, 0xF1, 0x35, 0x9A, 0xCB, 0x4D },
			AAD = new byte[] { 0xFA, 0x8, 0x89, 0x38 },
			PlainText = new byte[] { 0x6, 0xB7, 0x48, 0xDE, 0xAE, 0x38, 0x9F, 0x87, 0xE4, 0x41, 0xC0, 0xB1, 0x0, 0x5D, 0x58, 0x62 },
			CipherText = new byte[] { 0xD7, 0xEE, 0x6, 0x9E, 0x7D, 0x7F, 0x67, 0x51, 0xD1, 0x70, 0x9C, 0x6F, 0xCA, 0x9, 0xFF, 0xA0 },
			Tag = new byte[] { 0x9C, 0x17, 0xF, 0x4F, 0xB5, 0xA3, 0x66, 0x99, 0xAD, 0xCC, 0xDA, 0x56, 0xBA, 0xCB, 0xE6, 0xFF },
		},
		new TestVectorAE
		{
			Key = new byte[] { 0xC1, 0xE2, 0xB7, 0xBC, 0x8E, 0x23, 0x31, 0xD2, 0x2, 0xE8, 0xC4, 0xBA, 0xA8, 0x1B, 0xFB, 0xE4 },
			IV = new byte[] { 0x3, 0xC4, 0x11, 0xA9, 0xD2, 0x38, 0x5A, 0x27, 0x91, 0x8C, 0xDF, 0x4D },
			AAD = new byte[] { 0x43, 0x50, 0x48, 0x32, 0x6F, 0x7A, 0x0, 0xAB },
			PlainText = new byte[] { 0x69, 0xA1, 0x93, 0x1C, 0xD2, 0x6C, 0x5B, 0x8E, 0x62, 0x49, 0x5B, 0x2, 0xE5, 0x62, 0x6B, 0x5, 0x84, 0xCD, 0x96, 0x85, 0x46, 0x2, 0xC0, 0xE3 },
			CipherText = new byte[] { 0xB9, 0xE5, 0x4E, 0xE3, 0x1D, 0x4D, 0x2D, 0x5A, 0xD8, 0x9E, 0xEE, 0x93, 0x20, 0x9E, 0x5F, 0xAC, 0x5E, 0x26, 0x82, 0xAD, 0x4B, 0x7C, 0x3D, 0x51 },
			Tag = new byte[] { 0x81, 0x72, 0x19, 0x90, 0xAC, 0x35, 0xD9, 0x56, 0xB5, 0xA8, 0x65, 0x7B, 0xD7, 0xE2, 0x23, 0xFF },},
		new TestVectorAE
		{
			Key = new byte[] { 0xD7, 0x93, 0x8B, 0x49, 0x87, 0x17, 0x7C, 0x3E, 0x95, 0x67, 0x5A, 0x78, 0xAE, 0xE, 0xD7, 0xF1 },
			IV = new byte[] { 0xB1, 0xFA, 0x73, 0x3D, 0xB5, 0x78, 0x9C, 0x1A, 0x4D, 0x5F, 0x1A, 0x49 },
			AAD = new byte[] { 0xB, 0x8A, 0xAA, 0x9C, 0x42, 0x8C, 0xE9, 0x51, 0x10, 0xA0, 0x74, 0x9E },
			PlainText = new byte[] { 0x9C, 0xDF, 0x4E, 0x8D, 0x9D, 0xB6, 0x82, 0xA, 0x1C, 0x96, 0x9, 0x3A, 0x7A, 0xED, 0xED, 0xF, 0xB4, 0x56, 0xAF, 0x1A, 0xE6, 0xC, 0xE7, 0xA0, 0xE, 0xA5, 0x2A, 0xEF, 0x13, 0xFD, 0x30, 0xF2 },
			CipherText = new byte[] { 0x72, 0x37, 0xDE, 0xE3, 0xFC, 0x3, 0x86, 0xC0, 0xB1, 0xFF, 0xC, 0x17, 0x95, 0xEF, 0x9B, 0x35, 0x4, 0x88, 0x54, 0xBC, 0x8E, 0xF4, 0xC2, 0x1F, 0x37, 0x2E, 0xE, 0xED, 0xB7, 0x35, 0xC4, 0x92 },
			Tag = new byte[] { 0xFE, 0x27, 0x75, 0x84, 0x94, 0x42, 0xC, 0xCD, 0x4A, 0x69, 0x24, 0xE1, 0x85, 0x37, 0x8C, 0x8E },
		},
		new TestVectorAE
		{
			Key = new byte[] { 0x6, 0x70, 0x36, 0x4, 0xA7, 0x4C, 0xA2, 0x2, 0x2B, 0x85, 0xE6, 0xA4, 0x3C, 0xAB, 0xA6, 0xD },
			IV = new byte[] { 0x42, 0x1A, 0x1D, 0xA0, 0xE1, 0x71, 0xBD, 0x16, 0xC2, 0x45, 0x15, 0xCD },
			AAD = new byte[] { 0x97, 0x16, 0xF8, 0x63, 0x59, 0x81, 0xDC, 0x84, 0xD4, 0xB8, 0x80, 0x0, 0x66, 0x30, 0xD2, 0x21 },
			PlainText = new byte[] { 0x4, 0x99, 0xF, 0x3B, 0x7D, 0x13, 0x2C, 0x84, 0x3, 0x9F, 0x75, 0x26, 0xE7, 0xD3, 0x74, 0x93, 0xA4, 0xD3, 0x14, 0x19, 0x1F, 0x7C, 0x6, 0xB6, 0x2D, 0xE2, 0x64, 0x2D, 0x56, 0xFD, 0x94, 0x59, 0xD9, 0x9F, 0x70, 0xC2, 0xEF, 0xA9, 0xA3, 0x6E },
			CipherText = new byte[] { 0xD0, 0x7F, 0x6D, 0xB6, 0xC2, 0xDA, 0xF8, 0x44, 0x91, 0xC, 0xA8, 0x2C, 0x34, 0x17, 0x61, 0x17, 0xCF, 0x80, 0x48, 0xAF, 0xF0, 0x5, 0xF1, 0x82, 0x40, 0xBE, 0x8B, 0x68, 0xBF, 0xFE, 0xD2, 0x50, 0xFE, 0x4A, 0xEE, 0x85, 0x25, 0x43, 0x77, 0xCB },
			Tag = new byte[] { 0x41, 0x54, 0x98, 0x7B, 0x14, 0xCE, 0xA1, 0xD1, 0xDA, 0x75, 0xA4, 0x25, 0xF9, 0x8E, 0x77, 0xB6 },},
		new TestVectorAE
		{
			Key = new byte[] { 0x54, 0x7F, 0x56, 0xFA, 0x4, 0xB9, 0xD, 0xE9, 0x42, 0x1C, 0xCB, 0x3B, 0xDF, 0xC8, 0x7D, 0x41 },
			IV = new byte[] { 0xB9, 0xB2, 0x95, 0xB1, 0x82, 0x55, 0xF6, 0xF6, 0xC3, 0xA5, 0x10, 0xC9 },
			AAD = new byte[] { 0x20, 0x45, 0x4C, 0x5, 0xDD, 0xF9, 0xAA, 0xB7, 0xA1, 0x5E, 0xA8, 0x75, 0x7A, 0xD9, 0x67, 0xF2, 0x22, 0xCD, 0x9, 0xCF },
			PlainText = new byte[] { 0x81, 0x11, 0x5D, 0xB3, 0x72, 0xA1, 0xBE, 0x9D, 0xCA, 0x93, 0x38, 0x79, 0x97, 0x4D, 0xED, 0xF2, 0xA5, 0xF6, 0xB7, 0x88, 0x47, 0x2A, 0xF, 0xE2, 0x53, 0xF2, 0x2E, 0x65, 0x6F, 0xF3, 0x71, 0x5, 0x8B, 0xE5, 0x2, 0x5B, 0xAF, 0x47, 0xB5, 0xF8, 0xF7, 0x51, 0x30, 0xFA, 0xDC, 0x2B, 0x4F, 0xDD },
			CipherText = new byte[] { 0xFB, 0x74, 0x45, 0x2E, 0xFB, 0x7B, 0xD9, 0xB7, 0x85, 0x9B, 0xE3, 0xDF, 0x2C, 0x89, 0xB0, 0x1E, 0xFD, 0x75, 0x47, 0xA1, 0x4D, 0x33, 0xA5, 0x2, 0xC7, 0x80, 0x3A, 0x37, 0xA8, 0x29, 0x91, 0x37, 0x2, 0xCC, 0x6D, 0xBF, 0x8B, 0xF8, 0x20, 0xAE, 0xED, 0xAD, 0x70, 0x6A, 0x37, 0x59, 0xA, 0x1D },
			Tag = new byte[] { 0x92, 0x21, 0x51, 0x2C, 0xF0, 0xC0, 0xE4, 0xBD, 0xD, 0xEE, 0xDA, 0x67, 0x88, 0x15, 0xB3, 0x9C },
		},
		new TestVectorAE
		{
			Key = new byte[] { 0xB4, 0x5C, 0xC7, 0x93, 0x7C, 0x85, 0x9, 0xD6, 0xD6, 0x4F, 0x69, 0xBD, 0x4E, 0xA2, 0x7F, 0x8D },
			IV = new byte[] { 0xE1, 0x18, 0xCD, 0xD2, 0xC0, 0xB1, 0xA3, 0xAF, 0x51, 0xC8, 0x40, 0x73 },
			AAD = new byte[] { 0x77, 0x45, 0x3A, 0x6C, 0x13, 0x15, 0x20, 0x0, 0xCB, 0xD7, 0x5D, 0xF9, 0xA, 0xD8, 0x5F, 0x21, 0x47, 0x42, 0xF9, 0xF, 0x8B, 0x90, 0x6, 0x40 },
			PlainText = new byte[] { 0x2D, 0x45, 0x50, 0x6A, 0xE0, 0xDE, 0xB0, 0xC, 0xF1, 0x5, 0x76, 0x4A, 0xC9, 0x7, 0xA, 0xF8, 0xB9, 0x88, 0xD4, 0xF5, 0xF7, 0x76, 0x5A, 0xBA, 0xE3, 0x3B, 0xA8, 0x32, 0x8F, 0x37, 0xF5, 0xCD, 0x7F, 0x6D, 0x83, 0xD0, 0xAD, 0x7C, 0xC7, 0xEF, 0xD0, 0xD2, 0xCA, 0x1D, 0xDE, 0x1C, 0xCF, 0xA3, 0xD0, 0x0, 0x36, 0x6F, 0xA5, 0xF, 0xA0, 0xAF },    CipherText = new byte[] { 0x85, 0x70, 0xC, 0x6B, 0x73, 0x2E, 0xBE, 0x56, 0x76, 0x75, 0x6A, 0x37, 0x2E, 0xD9, 0x2, 0x52, 0x4, 0xF6, 0xC7, 0x88, 0xA3, 0x58, 0x1E, 0x8B, 0x88, 0x8E, 0xE5, 0x3D, 0x2B, 0x3E, 0x0, 0x62, 0x37, 0xCF, 0xF8, 0xE8, 0x8B, 0x85, 0xBD, 0xB6, 0x93, 0x86, 0xF9, 0xBD, 0xE6, 0x50, 0xF1, 0x40, 0xBC, 0x41, 0xCC, 0xB4, 0xC3, 0x76, 0x13, 0x6D },
			Tag = new byte[] { 0x3C, 0x52, 0x57, 0x86, 0x8C, 0xD6, 0xE5, 0x7A, 0x93, 0x35, 0x96, 0x50, 0xCA, 0xA2, 0xF3, 0x8E },},
		new TestVectorAE
		{
			Key = new byte[] { 0xA9, 0xF5, 0x54, 0x9F, 0xF7, 0xF3, 0xE2, 0x9F, 0x3D, 0x90, 0x6, 0x89, 0xBF, 0x8B, 0xCE, 0x41 },
			IV = new byte[] { 0x9F, 0x13, 0xFF, 0x8E, 0x5B, 0x3A, 0xB6, 0x28, 0x97, 0xA8, 0x4B, 0x12 },
			AAD = new byte[] { 0x1D, 0x8C, 0x7A, 0x29, 0x22, 0xD9, 0xC5, 0xFE, 0xCB, 0xAE, 0x14, 0xE1, 0xEF, 0x15, 0x9F, 0x3E, 0x1F, 0xF1, 0xD, 0x2D, 0xE3, 0x32, 0xDD, 0x44, 0x47, 0x4B, 0x6, 0xAF },
			PlainText = new byte[] { 0x96, 0xE6, 0xDA, 0x83, 0x66, 0xD6, 0x30, 0x50, 0xD4, 0x4D, 0xC2, 0xF6, 0x11, 0x4F, 0xD4, 0xB9, 0x1E, 0x66, 0x17, 0x97, 0xD3, 0x98, 0xCC, 0xD8, 0x26, 0x96, 0x2C, 0xE8, 0xAA, 0x36, 0x8, 0x62, 0x56, 0x23, 0xAD, 0x6D, 0x88, 0xD3, 0xA, 0x4B, 0x49, 0xBA, 0x15, 0x2F, 0xD3, 0xBE, 0x6A, 0x4F, 0x51, 0xEA, 0x35, 0x3B, 0xE5, 0x66, 0x53, 0x6D, 0xEF, 0xA7, 0xB4, 0x21, 0x8D, 0xE3, 0x4, 0x67 },
			CipherText = new byte[] { 0xA3, 0xAB, 0xC, 0xE0, 0x19, 0x3B, 0x7F, 0x39, 0xA3, 0x70, 0x59, 0x5C, 0xA, 0x32, 0x17, 0x8C, 0x55, 0x1F, 0xA3, 0xF9, 0xE7, 0xA9, 0x3, 0x49, 0xB0, 0xAD, 0x2, 0xB5, 0x48, 0x55, 0x86, 0xDE, 0x52, 0x62, 0x30, 0xE2, 0xB8, 0x12, 0x6D, 0xAA, 0x6B, 0x24, 0x64, 0x6F, 0xBA, 0xA1, 0xF8, 0xF3, 0xF3, 0xB8, 0x39, 0x73, 0xD5, 0xA2, 0x9E, 0xDE, 0x91, 0x98, 0xB5, 0x57, 0x5F, 0x65, 0x78, 0xBE },
			Tag = new byte[] { 0xA9, 0x93, 0xEA, 0x5, 0x4C, 0x1D, 0x32, 0xD9, 0x12, 0x55, 0x12, 0x99, 0x50, 0x60, 0x56, 0xCF },
		},
		new TestVectorAE
		{
			Key = new byte[] { 0x5E, 0xC8, 0x8A, 0x76, 0x8C, 0x6D, 0x50, 0x43, 0x9, 0x89, 0x84, 0x52, 0x18, 0xE3, 0x1E, 0xCC },
			IV = new byte[] { 0x10, 0x9, 0xC3, 0xD3, 0x5C, 0x45, 0xE9, 0x12, 0xC7, 0xCD, 0x3D, 0x98 },
			AAD = new byte[] { 0xF7, 0x3A, 0x41, 0xE6, 0x36, 0x3B, 0x3A, 0x46, 0x79, 0x8B, 0x49, 0x5F, 0x9A, 0x20, 0xD8, 0x7, 0xE9, 0xA6, 0x37, 0x58, 0xE3, 0x2B, 0x87, 0x6, 0x41, 0x1C, 0xD8, 0xEF, 0x35, 0x70, 0x8E, 0xF4 },
			PlainText = new byte[] { 0xF0, 0x23, 0x9, 0x8, 0x67, 0xE6, 0x85, 0x86, 0x57, 0x15, 0x3E, 0xC8, 0x86, 0xE9, 0xD8, 0xF5, 0x4D, 0xEF, 0xFF, 0x1A, 0x53, 0xB8, 0x4B, 0x7D, 0x9B, 0xD2, 0x66, 0x2E, 0xC1, 0xF8, 0x70, 0xDE, 0x80, 0x86, 0xA2, 0xF4, 0x78, 0x30, 0x32, 0xDE, 0x2E, 0xF2, 0x77, 0xA5, 0x55, 0x4A, 0x4E, 0xC5, 0x4C, 0xA6, 0xBC, 0x3B, 0xE7, 0x18, 0xD5, 0xDF, 0x72, 0x51, 0xDC, 0xF4, 0xF3, 0xCD, 0xAB, 0xFE, 0xB0, 0x4B, 0x55, 0xD3, 0xF1, 0x80, 0xB, 0xF5 },
			CipherText = new byte[] { 0x33, 0x30, 0x36, 0xBE, 0x62, 0x76, 0x53, 0x85, 0xED, 0x78, 0x16, 0x84, 0x4, 0x27, 0x27, 0x91, 0x72, 0xFF, 0xE2, 0x33, 0x55, 0x74, 0xF, 0xD1, 0x51, 0x53, 0xA1, 0xDD, 0x12, 0xCC, 0x11, 0x8C, 0x6A, 0x4F, 0xB6, 0xC2, 0x6, 0xBE, 0x82, 0xBC, 0xF1, 0x1C, 0x34, 0xB9, 0x3C, 0x8F, 0xC, 0x91, 0x98, 0x3B, 0x89, 0x8B, 0x8, 0xD4, 0xD0, 0x2B, 0xE8, 0x68, 0xE, 0x31, 0x24, 0x49, 0x94, 0x74, 0x9, 0x50, 0x25, 0xFC, 0xEA, 0x82, 0x5B, 0xCD },
			Tag = new byte[] { 0xE4, 0xB, 0x9C, 0xDB, 0x14, 0x4D, 0x9E, 0x60, 0x6, 0x7D, 0xA2, 0x58, 0x1B, 0x3, 0xD9, 0x3D },
		},
		new TestVectorAE
		{
			Key = new byte[] { 0x7, 0xC, 0x3C, 0x1F, 0x8D, 0xAD, 0x0, 0x1E, 0xEE, 0xB3, 0xB7, 0xE2, 0x28, 0xB4, 0xED, 0xD5 },
			IV = new byte[] { 0xCF, 0x80, 0x82, 0x6C, 0x54, 0x57, 0x7, 0xFB, 0x87, 0x5A, 0x6A, 0xCD },
			AAD = new byte[] { 0x5B, 0x40, 0xD6, 0x74, 0xE9, 0x4A, 0xD5, 0x5E, 0xB8, 0x79, 0xB8, 0xA9, 0x3C, 0xFE, 0x38, 0x38, 0x9C, 0xF2, 0x5D, 0x7, 0xB9, 0x47, 0x9F, 0xBB, 0x6B, 0xFF, 0x4C, 0x7E, 0xD, 0x9B, 0x29, 0x9, 0x3D, 0xD7, 0x5C, 0x2 },
			PlainText = new byte[] { 0xDD, 0x94, 0x89, 0x89, 0x5D, 0x16, 0x3C, 0xE, 0x3D, 0x6F, 0x87, 0x65, 0xCD, 0x3B, 0xEC, 0x1C, 0x38, 0x8E, 0x7C, 0xC, 0xC0, 0x2B, 0x41, 0x2E, 0x4B, 0xF7, 0xDA, 0xB0, 0x1F, 0xAD, 0x65, 0x48, 0xEA, 0xD2, 0xA2, 0xC9, 0x5, 0xEC, 0x54, 0xF4, 0xF9, 0xEF, 0xEB, 0x90, 0x43, 0xF8, 0x61, 0xBD, 0x54, 0x3D, 0x62, 0x85, 0xDC, 0x44, 0xAF, 0xB4, 0x48, 0x54, 0xC4, 0xE9, 0x89, 0x2A, 0xB9, 0xEE, 0x18, 0xEC, 0x66, 0x45, 0x37, 0x63, 0xCA, 0x3, 0x79, 0x64, 0xAE, 0xE2, 0x84, 0x8F, 0x85, 0x91 },
			CipherText = new byte[] { 0xB6, 0x34, 0x2E, 0x35, 0x28, 0xA0, 0x34, 0x30, 0xF3, 0x98, 0x25, 0x37, 0xC8, 0xB6, 0xA1, 0x84, 0xE9, 0x79, 0x9E, 0x80, 0xC0, 0x87, 0x5B, 0xA4, 0x9A, 0xC, 0x93, 0x0, 0x8, 0x3F, 0x51, 0x25, 0x6D, 0x73, 0x9D, 0x34, 0xA2, 0x63, 0x3E, 0x5B, 0x47, 0x53, 0x94, 0xF8, 0x1C, 0x78, 0x64, 0x6D, 0x3A, 0x96, 0xDD, 0x11, 0xEF, 0x23, 0x5B, 0xD4, 0x75, 0x8F, 0x6C, 0x6F, 0x97, 0xEA, 0xB, 0x89, 0xE9, 0x8B, 0xFB, 0x8A, 0x99, 0x66, 0x4E, 0x33, 0x17, 0xA, 0x63, 0xC4, 0xFE, 0x5C, 0xA3, 0xF8 },
			Tag = new byte[] { 0x87, 0xAF, 0x9D, 0x1B, 0xD0, 0x20, 0x8C, 0xD, 0x42, 0xCB, 0x77, 0x88, 0xDD, 0x3F, 0xE2, 0xDB },
		}
	};

		[Fact]
		public void LEA256_GCM_Encryption_AllTestVectorsPassing()
		{
			for (var i = 0; i < lea256GcmTestVectors.Length; i++)
			{
				// Arrange
				var testvector = lea256GcmTestVectors[i];
				var cipher = new Symmetric.Lea.Gcm();

				// Act
				cipher.Init(Mode.ENCRYPT, testvector.Key, testvector.IV, testvector.Tag.Length);
				cipher.UpdateAAD(testvector.AAD);
				ReadOnlySpan<byte> actual = cipher.DoFinal(testvector.PlainText);
				var tag = actual.Slice(actual.Length - 16, 16);
				var cipherText = actual.Slice(0, actual.Length - 16);

				// Assert
				Assert.True(cipherText.SequenceEqual(testvector.CipherText), "LEA-256-GCM encryption ciphertext test case #" + (i + 1));
				Assert.True(tag.SequenceEqual(testvector.Tag), "LEA-256-GCM encryption tag test case #" + (i + 1));
			}
		}

		[Fact]
		public void LEA256_GCM_Decryption_AllTestVectorsPassing()
		{
			for (var i = 0; i < lea256GcmTestVectors.Length; i++)
			{
				// Arrange
				var testvector = lea256GcmTestVectors[i];
				var cipher = new Symmetric.Lea.Gcm();

				// Act
				cipher.Init(Mode.DECRYPT, testvector.Key, testvector.IV, testvector.Tag.Length);
				cipher.UpdateAAD(testvector.AAD);
				var aggregated = new byte[testvector.CipherText.Length + testvector.Tag.Length];
				testvector.CipherText.CopyTo(aggregated, 0);
				testvector.Tag.CopyTo(aggregated, testvector.CipherText.Length);
				ReadOnlySpan<byte> actual = cipher.DoFinal(aggregated);

				// Assert
				Assert.True(actual.SequenceEqual(testvector.PlainText), "LEA-256-GCM decryption test case #" + (i + 1));
			}
		}

		[Fact]
		public void LEA192_GCM_Encryption_AllTestVectorsPassing()
		{
			for (var i = 0; i < lea192GcmTestVectors.Length; i++)
			{
				// Arrange
				var testvector = lea192GcmTestVectors[i];
				var cipher = new Symmetric.Lea.Gcm();

				// Act
				cipher.Init(Mode.ENCRYPT, testvector.Key, testvector.IV, testvector.Tag.Length);
				cipher.UpdateAAD(testvector.AAD);
				ReadOnlySpan<byte> actual = cipher.DoFinal(testvector.PlainText);
				var tag = actual.Slice(actual.Length - 16, 16);
				var cipherText = actual[..^16];

				// Assert
				Assert.True(cipherText.SequenceEqual(testvector.CipherText), "LEA-192-GCM encryption ciphertext test case #" + (i + 1));
				Assert.True(tag.SequenceEqual(testvector.Tag), "LEA-192-GCM encryption tag test case #" + (i + 1));
			}
		}

		[Fact]
		public void LEA192_GCM_Decryption_AllTestVectorsPassing()
		{
			for (var i = 0; i < lea192GcmTestVectors.Length; i++)
			{
				// Arrange
				var testvector = lea192GcmTestVectors[i];
				var cipher = new Symmetric.Lea.Gcm();

				// Act
				cipher.Init(Mode.DECRYPT, testvector.Key, testvector.IV, testvector.Tag.Length);
				cipher.UpdateAAD(testvector.AAD);
				var aggregated = new byte[testvector.CipherText.Length + testvector.Tag.Length];
				testvector.CipherText.CopyTo(aggregated, 0);
				testvector.Tag.CopyTo(aggregated, testvector.CipherText.Length);
				ReadOnlySpan<byte> actual = cipher.DoFinal(aggregated);

				// Assert
				Assert.True(actual.SequenceEqual(testvector.PlainText), "LEA-192-GCM decryption test case #" + (i + 1));
			}
		}

		[Fact]
		public void LEA128_GCM_Encryption_AllTestVectorsPassing()
		{
			for (var i = 0; i < lea128GcmTestVectors.Length; i++)
			{
				// Arrange
				var testvector = lea128GcmTestVectors[i];
				var cipher = new Symmetric.Lea.Gcm();

				// Act
				cipher.Init(Mode.ENCRYPT, testvector.Key, testvector.IV, testvector.Tag.Length);
				cipher.UpdateAAD(testvector.AAD);
				ReadOnlySpan<byte> actual = cipher.DoFinal(testvector.PlainText);
				var tag = actual.Slice(actual.Length - 16, 16);
				var cipherText = actual[..^16];

				// Assert
				Assert.True(cipherText.SequenceEqual(testvector.CipherText), "LEA-128-GCM encryption ciphertext test case #" + (i + 1));
				Assert.True(tag.SequenceEqual(testvector.Tag), "LEA-128-GCM encryption tag test case #" + (i + 1));
			}
		}

		[Fact]
		public void LEA128_GCM_Decryption_AllTestVectorsPassing()
		{
			for (var i = 0; i < lea128GcmTestVectors.Length; i++)
			{
				// Arrange
				var testvector = lea128GcmTestVectors[i];
				var cipher = new Symmetric.Lea.Gcm();

				// Act
				cipher.Init(Mode.DECRYPT, testvector.Key, testvector.IV, testvector.Tag.Length);
				cipher.UpdateAAD(testvector.AAD);
				var aggregated = new byte[testvector.CipherText.Length + testvector.Tag.Length];
				testvector.CipherText.CopyTo(aggregated, 0);
				testvector.Tag.CopyTo(aggregated, testvector.CipherText.Length);
				ReadOnlySpan<byte> actual = cipher.DoFinal(aggregated);

				// Assert
				Assert.True(actual.SequenceEqual(testvector.PlainText), "LEA-128-GCM decryption test case #" + (i + 1));
			}
		}
	}
}