using static LEA.BlockCipher;

namespace LEA.Test.OpMode
{
	public class CcmModeTest
	{
		private readonly TestVectorAE[] Lea256CcmTestVectors =
		{
		new TestVectorAE
		{
			Key = new byte[] { 0x18, 0x74, 0xBE, 0xF3, 0x86, 0xE4, 0x76, 0xF1, 0x5C, 0x34, 0x4F, 0x49, 0xEE, 0xF7, 0xE0, 0x44, 0x2E, 0xE2, 0x5B, 0x60, 0x74, 0x80, 0x6D, 0xA3, 0x7F, 0x27, 0x66, 0x2F, 0xB7, 0x2B, 0x9A, 0x17 },
			IV = new byte[] { 0xB4, 0x51, 0x7, 0x71, 0x4, 0x87, 0x38, 0xAC },
			AAD = Array.Empty<byte>(),
			PlainText = new byte[] { 0x29, 0xD2, 0x69, 0x24, 0xE2, 0x87, 0xEB, 0xF4 },
			CipherText = new byte[] { 0xCA, 0x66, 0x67, 0xB7, 0x11, 0xCF, 0xAA, 0x47 },
			Tag = new byte[] { 0xC8, 0xC1, 0x44, 0xE3, 0x7E, 0x26, 0x4B, 0x1E, 0x6F, 0x45, 0xD3, 0x80, 0xFE, 0xEA, 0xDE, 0x5D },
		},
		new TestVectorAE
		{
			Key = new byte[] { 0xAC, 0xBE, 0xCA, 0xB, 0x87, 0x4, 0xAE, 0x83, 0xFD, 0x50, 0xFF, 0x2E, 0x61, 0x39, 0x4C, 0x71, 0x55, 0x65, 0xEE, 0x55, 0x8E, 0x45, 0xE8, 0xDB, 0xDC, 0xFF, 0x12, 0x90, 0x64, 0xAC, 0x77, 0x62 },
			IV = new byte[] { 0x30, 0xC4, 0xFB, 0xB2, 0xEB, 0xD6, 0x41, 0xAA },
			AAD = new byte[] { 0xE5, 0x7D, 0xCB, 0xD1 },
			PlainText = new byte[] { 0x5E, 0x8F, 0x63, 0xDC, 0x0, 0x97, 0xBA, 0xC8, 0xAD, 0x86, 0x50, 0x25, 0x7B, 0xA5, 0x93, 0xB5 },
			CipherText = new byte[] { 0x70, 0x54, 0x27, 0xC9, 0xB, 0x25, 0x64, 0xA6, 0x59, 0x67, 0x66, 0x24, 0xFB, 0x1B, 0xE2, 0x64 },
			Tag = new byte[] { 0x9F, 0x2, 0x1A, 0x2D, 0xDE, 0xAD, 0xC, 0x32, 0xEB, 0x39, 0x46, 0x5D, 0x66, 0x6A, 0x39, 0x6F },
		},
		new TestVectorAE
		{
			Key = new byte[] { 0xFD, 0xCE, 0x4B, 0x35, 0xC5, 0xDC, 0x35, 0x7B, 0x26, 0x60, 0xEE, 0xC1, 0x3F, 0x96, 0x13, 0x41, 0x34, 0x97, 0xA7, 0x2, 0xBF, 0xCF, 0xB5, 0xF1, 0xE8, 0xB2, 0x36, 0x33, 0x10, 0xAB, 0x34, 0xBA },
			IV = new byte[] { 0x34, 0x55, 0x30, 0x16, 0x70, 0xEE, 0xC2, 0x8E },
			AAD = new byte[] { 0xC2, 0x9, 0x7F, 0x70, 0x11, 0x82, 0x1F, 0xA5 },
			PlainText = new byte[] { 0x6E, 0xB7, 0xE0, 0x88, 0x99, 0xF5, 0x25, 0xF9, 0xC7, 0x31, 0x63, 0xAC, 0xA2, 0xF9, 0x3D, 0xC7, 0x92, 0xAA, 0xEE, 0xAD, 0x3B, 0xE2, 0xE8, 0x14 },
			CipherText = new byte[] { 0x76, 0xBB, 0xDF, 0x87, 0xA9, 0x9, 0xDE, 0xA5, 0x41, 0xA, 0x9A, 0x59, 0x1E, 0x12, 0xB9, 0x8C, 0x33, 0xEF, 0x55, 0xA5, 0xD4, 0xA1, 0xB1, 0x9C },
			Tag = new byte[] { 0x33, 0xAB, 0xB0, 0xF6, 0x2F, 0x6E, 0x1B, 0x22, 0xE8, 0x35, 0xF0, 0x58, 0xBE, 0x21, 0x3B, 0xB8 },
		},
		new TestVectorAE
		{
			Key = new byte[] { 0x47, 0x38, 0xBA, 0x5E, 0x65, 0x2B, 0x37, 0x26, 0x91, 0x59, 0xD5, 0x19, 0x97, 0xF2, 0x53, 0x7, 0xD2, 0x6B, 0x9F, 0x39, 0x3A, 0x77, 0xF6, 0x6, 0x9E, 0x33, 0x4D, 0xA1, 0x2D, 0xA0, 0xF0, 0x35 },
			IV = new byte[] { 0x3B, 0x57, 0x69, 0x34, 0x42, 0x78, 0xA3, 0xEC },
			AAD = new byte[] { 0x27, 0xE4, 0x4F, 0x5B, 0xBF, 0xAC, 0x87, 0x44, 0x92, 0xC9, 0xB1, 0x82 },
			PlainText = new byte[] { 0xDF, 0xD, 0xA7, 0x9D, 0xE0, 0x59, 0x15, 0x9B, 0x4E, 0x11, 0x22, 0x18, 0x28, 0xCD, 0x52, 0x99, 0xB1, 0x51, 0xAA, 0x1E, 0xB8, 0xC0, 0x16, 0xF6, 0x1B, 0xC, 0x9, 0xD7, 0x8E, 0xB1, 0x63, 0x2E },
			CipherText = new byte[] { 0xF4, 0x97, 0x59, 0xFC, 0x38, 0x20, 0xE7, 0xFC, 0x2F, 0x6, 0x9E, 0x70, 0xFE, 0xB9, 0xB1, 0x21, 0x36, 0x9E, 0x7C, 0x54, 0xAA, 0xA0, 0x69, 0x17, 0x44, 0x6, 0xE7, 0x61, 0x78, 0xD0, 0xAE, 0xA7 },
			Tag = new byte[] { 0x86, 0xF4, 0xD3, 0x77, 0xF6, 0xB2, 0x2, 0x49, 0xCC, 0x95, 0xE5, 0xCC, 0x59, 0x9, 0x9E, 0x26 },
		},
		new TestVectorAE
		{
			Key = new byte[] { 0x99, 0xA3, 0x97, 0xB4, 0xE0, 0x84, 0xD, 0x42, 0xA7, 0x1B, 0xC5, 0xE6, 0x95, 0x2, 0x3E, 0x36, 0xB8, 0x9E, 0x96, 0xE4, 0xCA, 0x54, 0xF2, 0x93, 0xB3, 0xFA, 0x20, 0xE9, 0x4E, 0x8C, 0x7B, 0xE6 },
			IV = new byte[] { 0x70, 0x21, 0xF, 0x74, 0x27, 0x1D, 0xFE, 0x3F },
			AAD = new byte[] { 0x3F, 0xA7, 0x23, 0xC6, 0xB, 0x5E, 0xF4, 0xB3, 0x62, 0x47, 0xAD, 0x67, 0xE7, 0xA, 0xAB, 0xDA },
			PlainText = new byte[] { 0x8D, 0x5F, 0x15, 0xA2, 0x5B, 0xC3, 0xC2, 0x51, 0x6D, 0x6F, 0x57, 0x4, 0x3C, 0x8A, 0xB0, 0x39, 0x1F, 0x9E, 0x82, 0xE1, 0x11, 0x49, 0x3E, 0xB7, 0xB4, 0x35, 0x38, 0xCF, 0x96, 0x49, 0x8, 0x72, 0xB3, 0x21, 0x30, 0x29, 0x3C, 0xBC, 0x0, 0x18 },
			CipherText = new byte[] { 0xC6, 0xE3, 0x20, 0x3E, 0x7D, 0x57, 0xDF, 0x8D, 0xBB, 0x5A, 0xD0, 0x7B, 0x7D, 0x2D, 0x5D, 0x59, 0x4E, 0x5B, 0xDC, 0xE3, 0xD8, 0x30, 0xCA, 0xF, 0x91, 0x24, 0x3C, 0xC4, 0xCD, 0xE3, 0x23, 0x49, 0x4D, 0xD0, 0xB4, 0xEC, 0xD0, 0xC0, 0x55, 0xC3 },
			Tag = new byte[] { 0xDF, 0xF, 0x5D, 0x46, 0x9F, 0xB3, 0x31, 0x6D, 0xBE, 0xA5, 0xFD, 0xDA, 0xAD, 0x9A, 0x36, 0xFA },
		},
		new TestVectorAE
		{
			Key = new byte[] { 0x97, 0x75, 0xB7, 0x8D, 0xA7, 0x35, 0xEB, 0xD9, 0x4D, 0xE6, 0x2B, 0x5F, 0x2F, 0x2B, 0xE2, 0xEA, 0xB8, 0x4E, 0x7E, 0x44, 0x60, 0x7A, 0xD2, 0x4, 0x2D, 0x2A, 0xB9, 0xA7, 0x79, 0xE1, 0xFD, 0xE2 },
			IV = new byte[] { 0xF7, 0x1C, 0x77, 0x37, 0xF1, 0x84, 0x77, 0xC7 },
			AAD = new byte[] { 0xD3, 0x69, 0x66, 0x67, 0xED, 0x6C, 0xA9, 0xF5, 0x75, 0x7B, 0xC9, 0xED, 0xCB, 0x3, 0xD1, 0x37, 0x1, 0x60, 0xF8, 0x43 },
			PlainText = new byte[] { 0x9B, 0x99, 0x80, 0x58, 0x93, 0x4A, 0xDE, 0x29, 0xA1, 0xE1, 0x1B, 0xAA, 0xB9, 0xD, 0x7A, 0x21, 0xD4, 0x74, 0x5B, 0x80, 0xF3, 0xA5, 0x5F, 0xF2, 0x61, 0x4D, 0xCD, 0xB4, 0x39, 0xAD, 0x35, 0x26, 0x29, 0x4B, 0x53, 0x22, 0x75, 0xE8, 0x35, 0x8C, 0x3F, 0xB3, 0xA0, 0x2A, 0x5, 0xBC, 0xB1, 0xB6 },
			CipherText = new byte[] { 0x34, 0xAB, 0x14, 0xF0, 0x2B, 0x5E, 0xE1, 0xC6, 0xCB, 0xD2, 0x90, 0x48, 0xA4, 0xCC, 0xA4, 0xAB, 0x22, 0xBE, 0xC8, 0xF4, 0x35, 0x6C, 0x94, 0x73, 0x61, 0x95, 0xBA, 0x3C, 0xE8, 0xC8, 0xA2, 0x14, 0x4E, 0x2D, 0x42, 0x5F, 0xC0, 0x63, 0x5A, 0x7B, 0x71, 0x49, 0x6C, 0xF7, 0x30, 0x3D, 0xC6, 0xB3 },
			Tag = new byte[] { 0x63, 0x97, 0xEB, 0x0, 0x1D, 0xF7, 0x2D, 0x64, 0xC2, 0xE, 0xD7, 0x41, 0xA7, 0x9D, 0xAD, 0x2F },
		},
		new TestVectorAE
		{
			Key = new byte[] { 0x6B, 0xAB, 0xC0, 0x32, 0x3B, 0xB2, 0x89, 0x7C, 0xAA, 0xC0, 0x8E, 0x22, 0xDD, 0xF8, 0xB7, 0x77, 0xAB, 0x61, 0x3C, 0xE7, 0xA5, 0xDF, 0xC4, 0x87, 0x53, 0x62, 0xCF, 0xF0, 0xC1, 0x5D, 0x51, 0x4E },
			IV = new byte[] { 0x3A, 0x7A, 0x9F, 0xB4, 0x54, 0x8C, 0x8F, 0xB3 },
			AAD = new byte[] { 0x2D, 0xC4, 0xDB, 0x2A, 0xF3, 0x29, 0xC7, 0x5F, 0xAA, 0x42, 0x4, 0x4F, 0x29, 0x15, 0xD3, 0x43, 0x66, 0x56, 0x6E, 0xA4, 0xF4, 0xD8, 0xA3, 0x10 },
			PlainText = new byte[] { 0xCA, 0x47, 0xC2, 0xAD, 0x46, 0x19, 0xB9, 0xBE, 0x72, 0xC2, 0x7E, 0x74, 0x85, 0x27, 0xAD, 0x6, 0xAD, 0x56, 0x73, 0x4, 0xB, 0x74, 0xAA, 0xE8, 0xFF, 0xF6, 0x45, 0xEE, 0xA7, 0x15, 0xEF, 0x25, 0xA2, 0x7B, 0xEF, 0xC6, 0x1A, 0x43, 0x4F, 0xC5, 0x1, 0x20, 0x3D, 0xA7, 0x9A, 0xDC, 0xB1, 0x93, 0x3C, 0x5, 0x50, 0xAB, 0x53, 0xE3, 0x91, 0x9D },
			CipherText = new byte[] { 0x8C, 0x48, 0x9B, 0xB9, 0x6, 0x5F, 0x30, 0x0, 0xF9, 0x64, 0xAC, 0x1D, 0xED, 0x5E, 0x8B, 0x51, 0xB7, 0x69, 0x2A, 0x6C, 0x3F, 0xC7, 0xF9, 0xFB, 0xFB, 0x14, 0x79, 0x64, 0x8, 0x6C, 0x45, 0x46, 0x55, 0xCD, 0xB7, 0x81, 0x19, 0x4F, 0x9C, 0xA2, 0x64, 0x38, 0xE3, 0x96, 0x5F, 0xEC, 0x4D, 0x6, 0xB, 0x38, 0xF9, 0xBF, 0x7, 0xE7, 0xC1, 0x56 },
			Tag = new byte[] { 0xC8, 0x1B, 0x88, 0xF5, 0x1F, 0x87, 0x42, 0x5D, 0x6E, 0x60, 0x24, 0xD5, 0xDE, 0x8D, 0x57, 0x9A },
		},
		new TestVectorAE
		{
			Key = new byte[] { 0xA7, 0xAC, 0x1E, 0x74, 0xE1, 0xFB, 0x7D, 0xB5, 0x4B, 0x7F, 0xEF, 0xA7, 0xD7, 0xF0, 0x98, 0xF2, 0x63, 0x48, 0xE1, 0x6B, 0x3B, 0x2F, 0x1B, 0x2F, 0xDF, 0xB7, 0x65, 0xEF, 0x7, 0x49, 0xCF, 0x8A },
			IV = new byte[] { 0xE5, 0xE0, 0xBF, 0xE1, 0xAA, 0x74, 0x94, 0xE7 },
			AAD = new byte[] { 0x4B, 0xBA, 0x8A, 0xF7, 0x85, 0xA, 0xDD, 0x6B, 0x1F, 0xA0, 0x32, 0x6A, 0x6F, 0x8, 0x32, 0x1, 0x1F, 0xD4, 0x78, 0x75, 0x31, 0x90, 0xAD, 0xD8, 0x41, 0xF8, 0xF1, 0xDB },
			PlainText = new byte[] { 0xA, 0x68, 0x7D, 0xE3, 0x2C, 0x93, 0x87, 0x5F, 0x2D, 0x78, 0x66, 0x58, 0xBD, 0x92, 0xF7, 0x49, 0x3E, 0x4F, 0x3F, 0x32, 0x83, 0xC4, 0x52, 0xE8, 0xD8, 0x9F, 0x73, 0xAF, 0x43, 0x5D, 0x7C, 0xA, 0xA, 0xBD, 0x81, 0xD2, 0x75, 0x74, 0xB0, 0x86, 0xD5, 0x22, 0x7B, 0xE2, 0xC5, 0x85, 0xFA, 0xB3, 0xB0, 0xEC, 0x8A, 0xE9, 0x94, 0xEF, 0xBB, 0xEE, 0x5, 0x5E, 0x69, 0x36, 0x74, 0x77, 0x28, 0x18 },
			CipherText = new byte[] { 0x5, 0x39, 0x12, 0x32, 0xF2, 0x43, 0x5E, 0x9E, 0x26, 0x2, 0xE2, 0x14, 0x5F, 0xED, 0x38, 0x95, 0x13, 0x79, 0x84, 0x77, 0xC9, 0x55, 0xBC, 0x67, 0xD2, 0x84, 0x4D, 0xB7, 0x86, 0x9, 0x9F, 0x7, 0xE4, 0x90, 0x71, 0xD5, 0x86, 0x10, 0xDA, 0x54, 0x93, 0xA8, 0xC5, 0xC5, 0x19, 0x23, 0x58, 0x18, 0x9A, 0xAD, 0x86, 0x52, 0x2C, 0x8D, 0x67, 0x45, 0x1, 0x2F, 0xE2, 0x10, 0x13, 0x23, 0xD4, 0xA9 },
			Tag = new byte[] { 0x46, 0xF1, 0x5D, 0xCD, 0x1F, 0x8, 0xE4, 0xF6, 0x32, 0xF9, 0xE0, 0xBC, 0x98, 0x90, 0x2, 0x6B },
		},
		new TestVectorAE
		{
			Key = new byte[] { 0xB1, 0x5C, 0xE6, 0xDC, 0xAF, 0xC4, 0xCB, 0x15, 0x88, 0xF0, 0x64, 0x31, 0xC0, 0xE1, 0x1, 0x4E, 0xCE, 0xC8, 0x5B, 0x4F, 0xD8, 0xBE, 0x7A, 0x2F, 0xBF, 0xB5, 0xD6, 0x96, 0x5B, 0xAE, 0xBD, 0xAA },
			IV = new byte[] { 0x8, 0x2D, 0xF4, 0x28, 0x61, 0xEA, 0xA1, 0xB3 },
			AAD = new byte[] { 0x4B, 0xCA, 0x68, 0x6C, 0x36, 0xD, 0xD5, 0xC2, 0x9F, 0xC9, 0xFA, 0x8C, 0xD9, 0x97, 0x59, 0x55, 0xE, 0x8C, 0x4, 0xF6, 0x81, 0x6B, 0x2, 0x6B, 0x15, 0x18, 0xF5, 0xDF, 0x13, 0x50, 0xFA, 0x9 },
			PlainText = new byte[] { 0x28, 0x98, 0xD1, 0xBB, 0xAD, 0x73, 0x3E, 0xB8, 0x2B, 0xD8, 0xAE, 0xC7, 0x1F, 0xE3, 0x1E, 0x5, 0xFA, 0xCB, 0x39, 0x7F, 0x6C, 0x14, 0xC0, 0x0, 0xE2, 0x6, 0x2F, 0x28, 0x4E, 0x5D, 0x9E, 0xBE, 0x26, 0x42, 0xE5, 0x47, 0xAF, 0x7A, 0x3, 0xD8, 0x7B, 0xDE, 0xC1, 0x28, 0x30, 0xB, 0x92, 0xE9, 0x8C, 0x5A, 0xBE, 0x59, 0xA6, 0x13, 0xBF, 0x14, 0x77, 0xDD, 0xEB, 0x2C, 0x98, 0x79, 0x54, 0x7B, 0x4D, 0xB0, 0xED, 0x38, 0xC3, 0x8A, 0xEE, 0xC9 },
			CipherText = new byte[] { 0xEE, 0xE0, 0x1A, 0xE5, 0xB, 0x46, 0x2E, 0xA3, 0x48, 0xD0, 0xE4, 0x9D, 0xDC, 0x48, 0x60, 0x6D, 0x15, 0x9B, 0x36, 0xB4, 0x2, 0xBA, 0xEA, 0x23, 0xD8, 0x2C, 0xDC, 0x6A, 0xDC, 0xD3, 0x4, 0x2F, 0xA6, 0x6, 0xEE, 0xB9, 0xAA, 0x35, 0x3D, 0x2, 0x1B, 0xE1, 0xD1, 0xA6, 0xCC, 0x77, 0xE4, 0xB0, 0x87, 0x56, 0x43, 0x95, 0xE, 0x15, 0x2B, 0x18, 0x96, 0xAC, 0xDF, 0xAC, 0xF7, 0x2F, 0xF5, 0x50, 0x3D, 0x3C, 0x53, 0xC4, 0x59, 0xFE, 0x2A, 0x57 },
			Tag = new byte[] { 0x67, 0x9, 0x56, 0xC2, 0x1E, 0xB0, 0x3E, 0x3, 0x7, 0x22, 0xF7, 0x23, 0x8F, 0x23, 0xFC, 0x25 },
		},
		new TestVectorAE
		{
			Key = new byte[] { 0x96, 0xC1, 0x79, 0xD8, 0x96, 0x75, 0x7D, 0xA9, 0xCB, 0x21, 0xDB, 0xAB, 0xB7, 0xCE, 0xC7, 0x4D, 0xDA, 0x87, 0x71, 0x12, 0x7C, 0xC, 0xE5, 0xE8, 0xA6, 0xCB, 0x30, 0xB6, 0x72, 0xB, 0xC5, 0x34 },
			IV = new byte[] { 0x84, 0x6B, 0x1F, 0xFF, 0xD9, 0x8B, 0xA7, 0xED },
			AAD = new byte[] { 0xC0, 0x9, 0xD7, 0x9D, 0x25, 0xAF, 0xA0, 0x53, 0x15, 0xEA, 0x9, 0x3, 0x7F, 0x7E, 0x1A, 0xAC, 0x38, 0x79, 0xF8, 0xE8, 0x42, 0x65, 0x51, 0xC0, 0x4E, 0xC2, 0xFA, 0xC4, 0x7F, 0xB4, 0xD7, 0x5C, 0x71, 0xF8, 0xFC, 0x59 },
			PlainText = new byte[] { 0x7A, 0x1C, 0x11, 0xCF, 0x30, 0xF0, 0xFB, 0x27, 0x2B, 0x38, 0xB9, 0x70, 0xC, 0xA3, 0x8B, 0xF6, 0xBD, 0x1, 0x5A, 0x16, 0xFB, 0xB0, 0x54, 0x50, 0x12, 0x57, 0xD7, 0x9E, 0xE8, 0xD7, 0x90, 0x4, 0x3D, 0x7F, 0xE4, 0x69, 0x31, 0x80, 0x8E, 0xA5, 0x7, 0xA0, 0x8A, 0xDB, 0x28, 0x32, 0x32, 0x9, 0x6B, 0x45, 0xA8, 0xDD, 0x93, 0x1C, 0x73, 0xCB, 0x1A, 0xDE, 0x6D, 0x5C, 0x2D, 0x8F, 0xDC, 0xC9, 0xF7, 0x3F, 0xDF, 0xC8, 0x22, 0x81, 0xC, 0xA6, 0x9B, 0x1F, 0x57, 0x96, 0x96, 0xC, 0x35, 0x49 },
			CipherText = new byte[] { 0x7F, 0x37, 0x37, 0x79, 0x8D, 0x13, 0x80, 0xD7, 0x60, 0x4, 0x6B, 0xE6, 0x83, 0x8D, 0xD1, 0x74, 0xE9, 0x66, 0x3D, 0xE, 0xC2, 0x6D, 0xDE, 0x70, 0xA3, 0xFF, 0xA6, 0xA4, 0x6A, 0x2F, 0xB9, 0xD0, 0x1D, 0x25, 0x18, 0x97, 0x3C, 0xE0, 0xC3, 0x1C, 0x24, 0xB2, 0x75, 0x41, 0xE9, 0xBA, 0xF5, 0xEC, 0x89, 0x1, 0xD8, 0x43, 0xF3, 0x7A, 0x65, 0x94, 0xED, 0xF3, 0xB0, 0x2C, 0x26, 0x17, 0x98, 0x10, 0xE5, 0x94, 0x68, 0x36, 0xD6, 0x4E, 0xAF, 0xE0, 0xDF, 0xC5, 0x57, 0xEB, 0xEA, 0x2E, 0xD3, 0x35 },
			Tag = new byte[] { 0xD1, 0x9, 0x94, 0xE9, 0x44, 0x9, 0x7E, 0xBE, 0xDE, 0x83, 0x7E, 0x6E, 0xF1, 0xE5, 0x1, 0xBF },
		}
	};
		private readonly TestVectorAE[] Lea192CcmTestVectors =
		{
		new TestVectorAE
		{
			Key = new byte[] { 0x1B, 0xB5, 0x54, 0x60, 0xF2, 0xC5, 0xA1, 0x3F, 0x43, 0x4D, 0xD8, 0x6E, 0x7B, 0x97, 0x6A, 0xA9, 0x38, 0x54, 0x96, 0x42, 0x53, 0x8D, 0x8C, 0xBA },
			IV = new byte[] { 0x1E, 0xDC, 0xD3, 0x8E, 0xEA, 0xDB, 0xE8, 0x53 },
			AAD = Array.Empty<byte>(),
			PlainText = new byte[] { 0xCD, 0xDD, 0x28, 0x5, 0xA2, 0xDC, 0xEF, 0x9D },
			CipherText = new byte[] { 0x81, 0x1E, 0xB4, 0x2B, 0xCF, 0xF3, 0x9E, 0x42 },
			Tag = new byte[] { 0x17, 0xE8, 0x93, 0x50, 0xFA, 0xC5, 0x19, 0xF3, 0x9D, 0xFC, 0x24, 0x23, 0xCD, 0x35, 0xB1, 0x9B },
		},
		new TestVectorAE
		{
			Key = new byte[] { 0x50, 0xB3, 0x2, 0xE1, 0x50, 0x8D, 0x2A, 0x80, 0x69, 0x9E, 0x22, 0x5E, 0xBE, 0x11, 0xB3, 0x2D, 0xB, 0x41, 0x1E, 0x3D, 0x2C, 0x2B, 0xBA, 0x95 },
			IV = new byte[] { 0xDF, 0x24, 0xDE, 0x42, 0xCA, 0xC7, 0x1, 0xEF },
			AAD = new byte[] { 0xC0, 0x32, 0x20, 0xB6 },
			PlainText = new byte[] { 0x40, 0x72, 0x61, 0x36, 0x3E, 0x3B, 0xF5, 0xE6, 0x26, 0xDC, 0x42, 0xA8, 0x1F, 0x73, 0x42, 0xA2 },
			CipherText = new byte[] { 0xC3, 0xE4, 0xF, 0xA1, 0x3D, 0xE1, 0x35, 0xDB, 0x8C, 0x60, 0xC1, 0xFD, 0xC1, 0x5, 0x3F, 0x8C },
			Tag = new byte[] { 0x21, 0x41, 0x52, 0xC3, 0x5F, 0xC7, 0xF3, 0x7C, 0x9C, 0x9B, 0xC1, 0x1C, 0x57, 0xBF, 0x7D, 0xE4 },
		},
		new TestVectorAE
		{
			Key = new byte[] { 0x8D, 0x5D, 0x26, 0x49, 0xD9, 0xF1, 0x40, 0xBE, 0x74, 0x6E, 0x1F, 0x5E, 0x19, 0x31, 0xFC, 0x95, 0x53, 0x2C, 0x55, 0xF2, 0x40, 0x93, 0xD7, 0x81 },
			IV = new byte[] { 0x94, 0xD8, 0x81, 0x55, 0xE0, 0xDA, 0x7, 0x64 },
			AAD = new byte[] { 0x15, 0x5A, 0x74, 0x98, 0x44, 0xAE, 0x14, 0x22 },
			PlainText = new byte[] { 0xA7, 0xB9, 0x91, 0x12, 0xE5, 0x1A, 0x5C, 0x24, 0x92, 0xA4, 0x4E, 0xDD, 0xF5, 0xE, 0x90, 0xD7, 0xF, 0xEC, 0xB8, 0x5A, 0xD8, 0xDE, 0x34, 0x2B },
			CipherText = new byte[] { 0xE9, 0xDF, 0x41, 0x9C, 0xF7, 0xB4, 0xA5, 0x5, 0xE2, 0xAE, 0xFF, 0x8C, 0x99, 0x3B, 0xBD, 0xEB, 0x54, 0xE6, 0x78, 0x2A, 0xA7, 0x24, 0xE6, 0x6A },
			Tag = new byte[] { 0x3B, 0x4B, 0x35, 0x7, 0x5C, 0x10, 0xA2, 0x8, 0xE8, 0xC6, 0x3B, 0x8D, 0x5A, 0x51, 0x8B, 0xA3 },
		},
		new TestVectorAE
		{
			Key = new byte[] { 0xCC, 0x8, 0xEC, 0xA9, 0xA6, 0xC2, 0x23, 0x15, 0x6C, 0x90, 0x9E, 0x32, 0xAB, 0x54, 0x9, 0xE, 0x4, 0xC2, 0x96, 0x1F, 0xE7, 0x15, 0xE7, 0x91 },
			IV = new byte[] { 0x9F, 0x0, 0x2E, 0xDF, 0x8F, 0xE8, 0x93, 0xA1 },
			AAD = new byte[] { 0xBB, 0x50, 0xD6, 0xA5, 0x8A, 0x95, 0x35, 0x40, 0xCA, 0xDB, 0x90, 0xE6 },
			PlainText = new byte[] { 0xF4, 0x47, 0xCB, 0xF9, 0xB1, 0x6F, 0x74, 0xDF, 0x9E, 0xE, 0xA5, 0x57, 0x4C, 0xFD, 0x2C, 0xD, 0x8C, 0x5E, 0xEA, 0x6C, 0xE9, 0x1A, 0x79, 0xB3, 0x75, 0x8F, 0x12, 0x4B, 0xC9, 0x82, 0xAD, 0x58 },
			CipherText = new byte[] { 0xF5, 0x49, 0xC8, 0x53, 0xB0, 0x34, 0x7E, 0xA2, 0x9C, 0x9D, 0x3B, 0x35, 0xAD, 0x61, 0xE2, 0x96, 0x51, 0xE2, 0xCF, 0x66, 0x59, 0x2A, 0xA7, 0x1A, 0x2C, 0x34, 0x7C, 0xAE, 0x2, 0xE0, 0xD5, 0x75 },
			Tag = new byte[] { 0x73, 0x28, 0x10, 0xCF, 0xC5, 0xE5, 0xB5, 0x21, 0xE3, 0x60, 0xA3, 0x32, 0x1C, 0x46, 0xCB, 0x8E },
		},
		new TestVectorAE
		{
			Key = new byte[] { 0x47, 0x5B, 0x69, 0x8E, 0xA2, 0x91, 0x2F, 0x45, 0xA1, 0x63, 0x92, 0xD0, 0x1F, 0xC9, 0xFA, 0xB, 0x57, 0xAA, 0x2, 0x34, 0x5E, 0x4F, 0x1E, 0xFC },
			IV = new byte[] { 0xF1, 0x18, 0x81, 0xDD, 0x71, 0x6D, 0x3E, 0xAD },
			AAD = new byte[] { 0x2C, 0xF8, 0xAD, 0x74, 0xDE, 0x11, 0x10, 0x7B, 0x78, 0x7A, 0x76, 0xA5, 0x1F, 0x4D, 0xE0, 0x2 },
			PlainText = new byte[] { 0x78, 0x32, 0xA3, 0xA2, 0x23, 0xD5, 0xD, 0xA7, 0x85, 0x98, 0x4B, 0x9E, 0xCB, 0x85, 0x8A, 0x10, 0x2B, 0x86, 0x5D, 0x53, 0x6D, 0xD8, 0x5F, 0xA6, 0xAB, 0xBD, 0x19, 0x7E, 0x55, 0x71, 0x26, 0x1A, 0x77, 0x60, 0x94, 0x5D, 0x3F, 0x96, 0x8E, 0xDD },
			CipherText = new byte[] { 0xCD, 0x8E, 0x90, 0xA6, 0x89, 0x4F, 0xCE, 0xB6, 0x22, 0x60, 0x51, 0x8E, 0x17, 0x10, 0x71, 0xD1, 0xD5, 0x4A, 0x7F, 0xEB, 0x80, 0x3B, 0xF2, 0x8A, 0xC8, 0xF, 0xAB, 0x12, 0x9A, 0xE0, 0xAD, 0xC1, 0xF1, 0xBE, 0xF5, 0x8C, 0x8E, 0xA7, 0x7, 0xCA },
			Tag = new byte[] { 0x4D, 0x8, 0x23, 0x1C, 0x9E, 0xC7, 0x5F, 0x9D, 0x43, 0xED, 0x5B, 0x41, 0xDE, 0x29, 0xEB, 0xB3 },
		},
		new TestVectorAE
		{
			Key = new byte[] { 0xBA, 0x77, 0x38, 0x38, 0xDE, 0xB0, 0xDB, 0xA7, 0xCD, 0x6D, 0x42, 0xD5, 0x58, 0xCD, 0x64, 0xF1, 0xC2, 0x92, 0x43, 0x1, 0xC7, 0x1E, 0x13, 0x7A },
			IV = new byte[] { 0xDD, 0x9B, 0x9F, 0xE, 0xBA, 0x79, 0xE9, 0xC6 },
			AAD = new byte[] { 0x16, 0x1D, 0x10, 0x50, 0xB2, 0xD7, 0x77, 0x9A, 0x99, 0x15, 0x24, 0xA0, 0xA6, 0xD7, 0x66, 0x82, 0x46, 0x63, 0xBB, 0xD9 },
			PlainText = new byte[] { 0x7F, 0x3A, 0xF0, 0x4B, 0x32, 0x4A, 0x8A, 0x5D, 0x9D, 0x65, 0x65, 0x89, 0x23, 0xF2, 0x9B, 0x91, 0x50, 0xE3, 0xA7, 0x12, 0x18, 0xE7, 0xCB, 0x5C, 0x5F, 0xE0, 0xAF, 0x81, 0xBF, 0x97, 0x4A, 0xAC, 0x85, 0xAE, 0x5, 0xB4, 0xFC, 0xD7, 0x1E, 0xF1, 0xEF, 0x44, 0x4B, 0x4F, 0x69, 0x5F, 0xA1, 0xA0 },
			CipherText = new byte[] { 0xC4, 0x4, 0xA0, 0x60, 0xC8, 0x97, 0x93, 0x4F, 0x7E, 0x82, 0x2B, 0x4C, 0xC4, 0x47, 0x9F, 0x5B, 0x5A, 0x2F, 0x61, 0x8B, 0x9E, 0x93, 0x88, 0xDB, 0x6B, 0x9F, 0xA, 0xBE, 0xBE, 0x87, 0x5D, 0x37, 0x58, 0x34, 0xE6, 0xFF, 0xC9, 0x6E, 0x6A, 0xF2, 0x3E, 0x74, 0xBC, 0x21, 0xC7, 0x1B, 0x20, 0xF3 },
			Tag = new byte[] { 0x19, 0x89, 0xBD, 0x2F, 0x62, 0x10, 0xD5, 0x7A, 0x51, 0x76, 0xA8, 0xAD, 0xD4, 0xDF, 0x76, 0x4D },
		},
		new TestVectorAE
		{
			Key = new byte[] { 0xBF, 0xBA, 0xB3, 0x37, 0xD0, 0x49, 0xFE, 0xB2, 0x8C, 0xCF, 0x85, 0x17, 0xB3, 0x96, 0xB8, 0x23, 0xDF, 0x63, 0x9A, 0xDE, 0xC5, 0xA9, 0x23, 0x73 },
			IV = new byte[] { 0xB8, 0xFD, 0xE3, 0x3F, 0x6F, 0xCA, 0xE9, 0x28 },
			AAD = new byte[] { 0x7, 0x46, 0xE3, 0x30, 0x4B, 0xA4, 0x87, 0x49, 0x34, 0x8C, 0x31, 0x6C, 0xAC, 0xC8, 0xCD, 0xE5, 0x97, 0x40, 0xF7, 0xF1, 0x14, 0x28, 0x61, 0x8A },
			PlainText = new byte[] { 0x0, 0x78, 0x75, 0x82, 0xBD, 0x9C, 0x3C, 0xCF, 0x31, 0x6D, 0x7F, 0x26, 0x11, 0x61, 0x2C, 0xCB, 0x5F, 0xFE, 0xFA, 0xA7, 0x31, 0x95, 0x50, 0x9B, 0xB5, 0x2C, 0x64, 0x14, 0x72, 0xBC, 0xA0, 0xDF, 0xD0, 0x9D, 0x49, 0x3F, 0xDB, 0x3E, 0x62, 0x3D, 0x44, 0x19, 0xCE, 0x40, 0xA8, 0xE6, 0xB6, 0xDD, 0x15, 0xF, 0x2A, 0xF0, 0xCB, 0x65, 0xB, 0xEC },
			CipherText = new byte[] { 0xEA, 0x78, 0x4E, 0x44, 0x46, 0x73, 0x53, 0xD4, 0x86, 0xDA, 0xA, 0xF3, 0xA2, 0xFA, 0xC3, 0xD5, 0x99, 0x77, 0x45, 0xE9, 0x1B, 0x7, 0xBE, 0x39, 0x7F, 0xE7, 0x23, 0xEB, 0x4C, 0x6, 0x3, 0x70, 0x93, 0xBF, 0xBA, 0x38, 0x40, 0x14, 0xB4, 0x44, 0xF7, 0xA5, 0x50, 0x19, 0x18, 0x6, 0xCD, 0x6D, 0x60, 0x1E, 0x96, 0xC2, 0xB2, 0xBF, 0x24, 0xB7 },
			Tag = new byte[] { 0x20, 0x98, 0xEA, 0x38, 0x70, 0x26, 0xFD, 0x17, 0x97, 0x87, 0x0, 0xFE, 0x3E, 0x66, 0x2C, 0xBE },
		},
		new TestVectorAE
		{
			Key = new byte[] { 0x3F, 0x11, 0x28, 0x6F, 0x66, 0xCD, 0xA7, 0x1A, 0x2F, 0x54, 0xA7, 0x9F, 0x52, 0x16, 0x23, 0x1B, 0x48, 0x31, 0x19, 0xA7, 0x9A, 0x9D, 0xEC, 0x29 },
			IV = new byte[] { 0x5F, 0xA6, 0x8, 0x71, 0x0, 0xA0, 0xC4, 0x81 },
			AAD = new byte[] { 0x3F, 0xD6, 0x8C, 0x2B, 0x78, 0xC3, 0x3F, 0xE2, 0x26, 0x85, 0xD6, 0xCA, 0xA8, 0x8E, 0x47, 0x11, 0x46, 0x9D, 0xA8, 0x82, 0x6F, 0xEC, 0x98, 0x57, 0x44, 0xAB, 0xDB, 0x4F },
			PlainText = new byte[] { 0x5B, 0xAE, 0x44, 0x1, 0xBC, 0x63, 0xD8, 0x70, 0x30, 0x6, 0x30, 0xDF, 0xBC, 0x18, 0x1F, 0xE7, 0x68, 0xD, 0x95, 0xC6, 0x44, 0x45, 0xC3, 0xFA, 0xAD, 0x3D, 0x80, 0xF, 0xCD, 0x7, 0xEC, 0xB5, 0x61, 0xF7, 0x53, 0x15, 0xBF, 0xA0, 0x54, 0x65, 0x17, 0xFE, 0x42, 0xBA, 0xF9, 0xF5, 0xCA, 0x1E, 0x17, 0xFA, 0xD6, 0xE3, 0x4D, 0x11, 0xB2, 0x37, 0xDE, 0xF7, 0x6D, 0xF8, 0x1, 0xA0, 0x83, 0xC9 },
			CipherText = new byte[] { 0x37, 0x7A, 0x5, 0x6B, 0x3F, 0xAF, 0x20, 0xF0, 0x48, 0xF8, 0x15, 0xD9, 0x1B, 0xC3, 0x7F, 0x91, 0x29, 0xB3, 0xA9, 0xF, 0x57, 0xE1, 0xF9, 0x27, 0xF0, 0x5F, 0xEB, 0xA, 0xD4, 0x2C, 0xFA, 0xDC, 0xA5, 0x55, 0xB8, 0x10, 0x8, 0xE1, 0x85, 0xCA, 0x90, 0x34, 0x5F, 0x26, 0x3E, 0x63, 0x21, 0xF7, 0x62, 0xBE, 0x78, 0x43, 0xD5, 0xB8, 0x52, 0xCE, 0xF2, 0xF8, 0x47, 0xF4, 0x39, 0x32, 0x4, 0xA0 },
			Tag = new byte[] { 0xCE, 0x36, 0xC3, 0x44, 0x86, 0x5A, 0x98, 0x6C, 0x83, 0xC4, 0x5B, 0x9D, 0xA9, 0x6, 0xDD, 0xEE },
		},
		new TestVectorAE
		{
			Key = new byte[] { 0x9C, 0xE4, 0xB8, 0x65, 0x4F, 0x75, 0x47, 0x33, 0xB4, 0x14, 0x3C, 0x1B, 0xE5, 0x4, 0x1F, 0x99, 0x3F, 0xBF, 0xD2, 0xF, 0x66, 0xE9, 0xBC, 0x60 },
			IV = new byte[] { 0x89, 0x82, 0x28, 0xBA, 0xE8, 0x5F, 0xE7, 0xB3 },
			AAD = new byte[] { 0x93, 0xDB, 0x3D, 0x98, 0x74, 0xCA, 0xFB, 0x1, 0x8F, 0xAE, 0xE8, 0xA9, 0x89, 0x2E, 0x65, 0x7D, 0x67, 0xC3, 0x51, 0xF3, 0x99, 0xB5, 0x2B, 0x9D, 0x37, 0x46, 0x76, 0xFE, 0x8A, 0x2C, 0x62, 0x9C },
			PlainText = new byte[] { 0xCE, 0x45, 0xA8, 0x56, 0x36, 0x87, 0xB6, 0xF8, 0x31, 0x36, 0xC, 0xD, 0xEC, 0x59, 0xE6, 0xF5, 0x18, 0x6E, 0x1B, 0x37, 0xED, 0x5D, 0x45, 0x18, 0x6F, 0xAB, 0x22, 0x6C, 0xF0, 0xF0, 0x3A, 0xAC, 0xD6, 0x8B, 0xC2, 0x49, 0x9E, 0x94, 0xC1, 0x40, 0x21, 0x12, 0x71, 0xEF, 0x16, 0x6E, 0xE8, 0x26, 0xD9, 0x28, 0xF7, 0x82, 0x6B, 0xC9, 0x53, 0xF6, 0xB9, 0x19, 0xF2, 0xAB, 0x21, 0x91, 0xB8, 0xA, 0x32, 0x13, 0x53, 0x17, 0xB3, 0xD9, 0x64, 0x0 },
			CipherText = new byte[] { 0x3F, 0x8D, 0xC6, 0x96, 0xD0, 0x89, 0x1A, 0xF7, 0x9C, 0xF6, 0x3E, 0x86, 0x54, 0x7, 0x9, 0x6C, 0x3E, 0x73, 0x6E, 0xB5, 0x47, 0xEE, 0xA3, 0xE8, 0x78, 0x65, 0x83, 0x69, 0x8, 0x52, 0xC0, 0xB, 0x56, 0x5D, 0xD0, 0xAA, 0x29, 0x4C, 0x7A, 0x13, 0x53, 0x77, 0x9C, 0x32, 0x4, 0x6F, 0x43, 0x6C, 0xF6, 0xE0, 0xBC, 0x6E, 0xE3, 0x57, 0x2E, 0xA9, 0xE8, 0xA3, 0x9B, 0xF6, 0x30, 0xA7, 0xBD, 0xF3, 0xE2, 0xCD, 0xBC, 0xAA, 0x22, 0x40, 0x19, 0xAA },
			Tag = new byte[] { 0x63, 0xAF, 0x73, 0x95, 0x50, 0x40, 0x7F, 0x64, 0x86, 0x25, 0x1B, 0xBA, 0x38, 0x71, 0x7A, 0xBC },
		},
		new TestVectorAE
		{
			Key = new byte[] { 0xE7, 0xAC, 0xDF, 0xF5, 0x10, 0x56, 0xB4, 0x3B, 0x32, 0x59, 0xAF, 0x7C, 0x19, 0xE1, 0x9C, 0x62, 0x9D, 0xF8, 0xB0, 0x64, 0x25, 0xF9, 0x24, 0xDF },
			IV = new byte[] { 0x6A, 0x47, 0x23, 0x68, 0x7E, 0x3E, 0xE6, 0x61 },
			AAD = new byte[] { 0xC, 0x6A, 0x1E, 0xCA, 0xE1, 0xF7, 0x92, 0x5A, 0xAB, 0x69, 0x9, 0xCE, 0x1, 0x8E, 0x6A, 0x82, 0xA4, 0xB0, 0x22, 0x3E, 0x5, 0xE, 0xC6, 0x52, 0x7, 0x8E, 0xF9, 0xE5, 0x5, 0xAC, 0x91, 0x8A, 0x17, 0xB6, 0xF5, 0x14 },
			PlainText = new byte[] { 0x91, 0x3B, 0xC9, 0xAC, 0xCD, 0xC1, 0xCE, 0x20, 0x2B, 0xB6, 0x3, 0x7E, 0x55, 0xA9, 0x11, 0xE2, 0xC8, 0xBA, 0xE5, 0x8C, 0x90, 0x4F, 0x9F, 0x35, 0x52, 0x9, 0xAB, 0xA3, 0x90, 0xF7, 0x2D, 0x7C, 0x29, 0x9, 0xA3, 0x58, 0x1F, 0xC7, 0xC6, 0x19, 0x9D, 0x42, 0xB0, 0x7F, 0x39, 0x4B, 0x44, 0xF7, 0x76, 0xE4, 0xCB, 0xB4, 0x8D, 0xF4, 0xDB, 0x1, 0x6D, 0x3F, 0x7D, 0xD6, 0x0, 0x94, 0x8E, 0xA9, 0xB0, 0x48, 0x66, 0x86, 0x2A, 0xE4, 0xB8, 0x62, 0x61, 0x1B, 0xB9, 0xAD, 0xD6, 0xFF, 0x85, 0x26 },
			CipherText = new byte[] { 0x9C, 0x19, 0xA6, 0x87, 0xDD, 0xD1, 0x18, 0x30, 0x3A, 0x23, 0x53, 0xC2, 0x45, 0x67, 0xE2, 0x46, 0xFE, 0x43, 0x30, 0x31, 0x30, 0x2D, 0xB7, 0xFC, 0xA, 0x8, 0x89, 0xCF, 0x6D, 0xCA, 0xE5, 0xD4, 0xA4, 0x4C, 0x6D, 0x5, 0x45, 0xDA, 0xCA, 0xBF, 0x7, 0x4D, 0xA5, 0xC9, 0x1, 0x3A, 0xCA, 0x42, 0x1B, 0x2D, 0xE0, 0x29, 0xA7, 0xE1, 0x84, 0xC, 0xC, 0x2B, 0x86, 0x39, 0x2, 0x2, 0x31, 0x4F, 0xEC, 0x7E, 0xA5, 0xBA, 0x37, 0xE0, 0xCD, 0x42, 0x2D, 0x5, 0x61, 0x32, 0xB3, 0xD9, 0xE8, 0x2B },
			Tag = new byte[] { 0x6, 0xF2, 0xEE, 0x36, 0x31, 0x66, 0xF, 0xC5, 0xF7, 0x63, 0x3B, 0xDA, 0x92, 0xB1, 0x63, 0x73 },
		}
	};
		private readonly TestVectorAE[] Lea128CcmTestVectors =
		{
		new TestVectorAE
		{
			Key = new byte[] { 0x67, 0xF, 0xD2, 0x86, 0xDF, 0x28, 0x3C, 0x66, 0x2D, 0xB8, 0x64, 0xA6, 0x81, 0xB9, 0xAB, 0x35 },
			IV = new byte[] { 0xE5, 0x9E, 0x5, 0x4A, 0x7E, 0x8B, 0x58, 0x40 },
			AAD = Array.Empty<byte>(),
			PlainText = new byte[] { 0xE, 0xC5, 0x26, 0xA3, 0xBE, 0x68, 0x6C, 0x8B },
			CipherText = new byte[] { 0x90, 0xB7, 0x61, 0x8D, 0x8A, 0x50, 0x72, 0x3C },
			Tag = new byte[] { 0xE3, 0xE9, 0x85, 0xF0, 0xD9, 0xA5, 0x9D, 0xB0, 0xB7, 0xB4, 0xEF, 0x63, 0x19, 0x4D, 0x62, 0xFB },
		},
		new TestVectorAE
		{
			Key = new byte[] { 0x1B, 0x58, 0xCA, 0x28, 0x5E, 0x79, 0xE3, 0x9F, 0xF0, 0x23, 0x7, 0x14, 0x2D, 0xD0, 0x10, 0x8 },
			IV = new byte[] { 0x69, 0x18, 0x89, 0xE6, 0x30, 0xE0, 0x16, 0xD8 },
			AAD = new byte[] { 0xD2, 0xCE, 0xF, 0xDF },
			PlainText = new byte[] { 0x1F, 0x0, 0x6F, 0x23, 0x70, 0xFD, 0xED, 0x88, 0xE7, 0xEB, 0x68, 0x81, 0xF7, 0x3, 0x88, 0x29 },
			CipherText = new byte[] { 0x17, 0xB9, 0x8E, 0x24, 0x9A, 0xB4, 0x4B, 0x6, 0x27, 0xD8, 0x34, 0x92, 0x6D, 0x46, 0x8A, 0x72 },
			Tag = new byte[] { 0xFE, 0x94, 0xFD, 0xEC, 0x35, 0x6F, 0x24, 0x40, 0x4, 0xF1, 0x80, 0x7B, 0x97, 0x3B, 0x61, 0xD },
		},
		new TestVectorAE
		{
			Key = new byte[] { 0x26, 0x43, 0xA3, 0x90, 0x11, 0x62, 0xDD, 0x52, 0xB1, 0x9, 0x94, 0x6F, 0xDE, 0x40, 0xFB, 0x56 },
			IV = new byte[] { 0x11, 0x27, 0x4, 0x43, 0x6E, 0xE6, 0x84, 0xCB },
			AAD = new byte[] { 0x2, 0xE6, 0x92, 0xB6, 0x49, 0x42, 0x2F, 0xC },
			PlainText = new byte[] { 0xF3, 0x78, 0x2D, 0x72, 0xE7, 0x65, 0x49, 0xB7, 0x89, 0x70, 0x9B, 0xAB, 0x38, 0x5, 0x30, 0x30, 0x1B, 0x45, 0xF5, 0x93, 0x1E, 0xFB, 0xD6, 0x1C },
			CipherText = new byte[] { 0x34, 0xB7, 0x49, 0xDF, 0xA3, 0x30, 0xCB, 0xF3, 0xC, 0xC7, 0x68, 0xE1, 0xCE, 0xB7, 0xA7, 0x55, 0xD8, 0xEC, 0x78, 0x3D, 0xDF, 0xEE, 0x17, 0x10 },
			Tag = new byte[] { 0x70, 0xF9, 0x20, 0xAD, 0x7B, 0xE2, 0x19, 0x14, 0xEA, 0x49, 0x4C, 0x4E, 0x1D, 0xDD, 0xC6, 0xB6 },
		},
		new TestVectorAE
		{
			Key = new byte[] { 0x37, 0xD2, 0x28, 0xE3, 0xEE, 0xC6, 0x44, 0xE6, 0x1C, 0xD9, 0x75, 0x59, 0xF3, 0x7, 0x15, 0x3D },
			IV = new byte[] { 0x3E, 0x7B, 0xF2, 0x34, 0xEA, 0x3, 0xF7, 0x94 },
			AAD = new byte[] { 0xC6, 0xC2, 0x8, 0xBA, 0x87, 0x25, 0x2D, 0xA1, 0xF, 0x18, 0xF3, 0xB3 },
			PlainText = new byte[] { 0xEF, 0x79, 0xF, 0xDE, 0x2A, 0xCC, 0x45, 0xEB, 0x3C, 0x31, 0x88, 0xB, 0x5A, 0x94, 0x63, 0xB3, 0xFC, 0x24, 0x97, 0xAA, 0x99, 0x6, 0x6, 0x61, 0xB1, 0x4A, 0xCF, 0x82, 0x11, 0x1E, 0xBB, 0xCB },
			CipherText = new byte[] { 0x4C, 0xBA, 0x88, 0x60, 0x51, 0xFE, 0xFD, 0x51, 0x89, 0x6A, 0x24, 0xBE, 0xCD, 0x52, 0x45, 0xE5, 0x39, 0xDB, 0xBD, 0xD2, 0x42, 0x61, 0xA3, 0xFF, 0x9B, 0xFA, 0x0, 0xF3, 0xB, 0xAB, 0x95, 0x84 },
			Tag = new byte[] { 0x1A, 0xBA, 0x3E, 0x51, 0xF6, 0x1C, 0xC1, 0xFB, 0x92, 0x80, 0xE4, 0x25, 0x74, 0x15, 0xB4, 0xE7 },
		},
		new TestVectorAE
		{
			Key = new byte[] { 0x53, 0x41, 0xD8, 0x1D, 0xA4, 0xBB, 0x63, 0xD0, 0xCA, 0xCC, 0xEB, 0x4A, 0x65, 0x22, 0xB4, 0xD0 },
			IV = new byte[] { 0x74, 0x4E, 0x5A, 0x9B, 0x1C, 0x79, 0xB6, 0x2F },
			AAD = new byte[] { 0xA5, 0x53, 0x5, 0xE8, 0xC6, 0x4D, 0x47, 0xD6, 0x22, 0x8, 0xE6, 0xCB, 0x12, 0xD, 0x98, 0xB1 },
			PlainText = new byte[] { 0xA5, 0xBE, 0x86, 0x21, 0xE5, 0x8D, 0xAE, 0x32, 0x5C, 0x6B, 0x86, 0x8F, 0xD7, 0x83, 0xE2, 0xCD, 0x6A, 0x29, 0x17, 0xFB, 0xB4, 0xD, 0x61, 0x7A, 0x64, 0xB5, 0x82, 0xF, 0xFC, 0x2A, 0xAC, 0x36, 0xD6, 0xF1, 0xA1, 0xB9, 0x64, 0x3C, 0x19, 0x82 },
			CipherText = new byte[] { 0x3C, 0xCA, 0x6A, 0xA1, 0xCC, 0x5E, 0x66, 0xAA, 0x22, 0xFB, 0xD8, 0x93, 0xAB, 0x1B, 0xA9, 0xB, 0xF2, 0xB8, 0x12, 0x56, 0x6D, 0xE2, 0x9, 0xCD, 0x69, 0xFE, 0x61, 0xE2, 0x50, 0x6F, 0x78, 0x3B, 0x54, 0x1, 0x3C, 0x23, 0x9E, 0xE5, 0xEA, 0xB0 },
			Tag = new byte[] { 0xB6, 0x6D, 0xAB, 0x38, 0x91, 0x79, 0xC0, 0xF2, 0x53, 0x42, 0x11, 0x72, 0x8F, 0xD6, 0x23, 0x4 },
		},
		new TestVectorAE
		{
			Key = new byte[] { 0xBB, 0x62, 0x64, 0xAD, 0x53, 0x8D, 0x50, 0xE2, 0x8, 0xB2, 0x4D, 0xE2, 0x65, 0xC7, 0xCF, 0x5F },
			IV = new byte[] { 0x22, 0x30, 0xF4, 0x11, 0xEB, 0x7A, 0x67, 0x6 },
			AAD = new byte[] { 0x64, 0x4B, 0x84, 0x3A, 0x69, 0x99, 0xBD, 0x67, 0x9A, 0x1B, 0x3C, 0x1E, 0x17, 0xFD, 0xC9, 0x17, 0x88, 0xD8, 0xEF, 0xC6 },
			PlainText = new byte[] { 0x61, 0xE1, 0x5A, 0x2E, 0x64, 0x61, 0x84, 0xFA, 0x67, 0x65, 0x91, 0xCA, 0x57, 0xB6, 0x77, 0xCB, 0x7D, 0xB, 0x61, 0x8A, 0x0, 0x7E, 0x56, 0xA, 0xAB, 0x3D, 0x15, 0x14, 0xF1, 0x82, 0x44, 0xCE, 0x2E, 0xC5, 0xCA, 0x8F, 0x30, 0x30, 0x7A, 0xF2, 0xF6, 0x53, 0xF1, 0x29, 0xB7, 0x19, 0xC1, 0x94 },
			CipherText = new byte[] { 0xFF, 0xD8, 0x5, 0x7, 0x30, 0xC3, 0x7F, 0x3B, 0x45, 0xDB, 0x1, 0x50, 0xC0, 0x29, 0x8, 0x86, 0x9B, 0x9, 0x4C, 0x11, 0xC4, 0xB9, 0xAC, 0xE0, 0x9B, 0x6A, 0x0, 0x47, 0xBD, 0x10, 0xF0, 0x8E, 0xB8, 0xF3, 0x4D, 0x9F, 0x5D, 0xA6, 0xEA, 0xB1, 0xEE, 0x2, 0x7, 0x89, 0x10, 0x1E, 0x41, 0x49 },
			Tag = new byte[] { 0x1A, 0x56, 0xE, 0xD8, 0x8E, 0x95, 0xC8, 0x9B, 0x6D, 0x44, 0x14, 0x63, 0x54, 0x39, 0x9D, 0x62 },
		},
		new TestVectorAE
		{
			Key = new byte[] { 0x27, 0xFA, 0xCF, 0x92, 0xF5, 0x73, 0xF9, 0x98, 0x98, 0xB3, 0xFC, 0x80, 0x4B, 0x98, 0x95, 0x23 },
			IV = new byte[] { 0x56, 0xD9, 0xE7, 0x29, 0x90, 0x85, 0x1D, 0x20 },
			AAD = new byte[] { 0x22, 0xAC, 0x50, 0xCC, 0x8D, 0x5B, 0xAB, 0xBE, 0xEF, 0xCE, 0xCD, 0x28, 0x82, 0xE2, 0x6E, 0xF0, 0x65, 0xC9, 0x46, 0x3, 0x9F, 0x1, 0x2A, 0xEC },
			PlainText = new byte[] { 0x61, 0x87, 0x79, 0xA5, 0x8B, 0x48, 0xB6, 0xAD, 0x72, 0xD8, 0x76, 0x5A, 0xE0, 0x66, 0x7D, 0x71, 0x5D, 0xF0, 0x24, 0xF4, 0xAD, 0xB3, 0xFE, 0x3B, 0x9A, 0xE6, 0xBA, 0x46, 0xF4, 0xA7, 0x4B, 0x52, 0xD5, 0x36, 0x47, 0xAA, 0x29, 0x61, 0x8D, 0xC0, 0x6E, 0x3F, 0x2B, 0x7C, 0xB9, 0x21, 0x7F, 0xD4, 0xFA, 0xC6, 0x9C, 0x9D, 0x80, 0xCE, 0xEE, 0xA1 },
			CipherText = new byte[] { 0xD5, 0xEA, 0x35, 0x92, 0xA9, 0xC4, 0xE4, 0x89, 0x33, 0x1D, 0xAC, 0x5C, 0x52, 0xBF, 0x5A, 0xF3, 0xB6, 0x55, 0x31, 0x66, 0x54, 0xEF, 0x3D, 0x2B, 0x45, 0xC4, 0x73, 0x3E, 0xE5, 0x17, 0x12, 0x50, 0xD7, 0xDD, 0xC, 0x3B, 0x50, 0xCA, 0x84, 0x7F, 0xA8, 0xA9, 0x24, 0xAF, 0xA, 0xA6, 0x8A, 0xB3, 0x3E, 0xF8, 0x39, 0xEB, 0x7F, 0x25, 0x97, 0x9F },
			Tag = new byte[] { 0xFA, 0xCE, 0x22, 0x3E, 0xBF, 0x81, 0x4F, 0x2F, 0x6A, 0x4, 0x8B, 0x89, 0x25, 0xA1, 0xD1, 0xE0 },
		},
		new TestVectorAE
		{
			Key = new byte[] { 0xC0, 0x71, 0x22, 0xF1, 0x52, 0x71, 0x40, 0x7D, 0x3C, 0x4D, 0x1A, 0xE1, 0x73, 0xB8, 0xEA, 0x82 },
			IV = new byte[] { 0x9F, 0x49, 0x37, 0xDA, 0x1F, 0x74, 0xF8, 0x10 },
			AAD = new byte[] { 0x4D, 0xB9, 0xFC, 0xCC, 0x3, 0x1B, 0x89, 0x49, 0x5E, 0xD4, 0x44, 0xAC, 0xC1, 0x47, 0x50, 0x94, 0x1D, 0x36, 0xBA, 0xF2, 0x70, 0x35, 0x15, 0x1B, 0x8C, 0x5F, 0x61, 0x1B },
			PlainText = new byte[] { 0x1A, 0x47, 0x32, 0x4C, 0xDE, 0x32, 0xE, 0x28, 0x3D, 0x83, 0x16, 0x5C, 0xFC, 0x76, 0xE8, 0x1D, 0x4A, 0x11, 0xC5, 0xBF, 0x1, 0x58, 0xEF, 0x84, 0x29, 0xC1, 0x55, 0xB2, 0xBE, 0xE7, 0x72, 0xE8, 0xB2, 0x82, 0x73, 0xB1, 0x36, 0x97, 0x98, 0x5A, 0x36, 0xEC, 0xDC, 0x1, 0x22, 0xBF, 0xD8, 0xEF, 0xF4, 0xB7, 0xE5, 0x28, 0x6F, 0x1B, 0x81, 0x3F, 0xA4, 0x42, 0xF4, 0x6F, 0xBB, 0x4B, 0x33, 0xE8 },
			CipherText = new byte[] { 0x16, 0xF5, 0xA0, 0x19, 0xD3, 0xAD, 0xB, 0xF3, 0xF0, 0xB2, 0xEC, 0x4A, 0x5B, 0x75, 0x97, 0x5B, 0x93, 0x10, 0x7E, 0xFA, 0x5E, 0xFE, 0xE, 0x95, 0xC3, 0xAD, 0x98, 0x81, 0x6C, 0x17, 0x87, 0x4F, 0xB9, 0x28, 0xF0, 0x1D, 0xB2, 0xB4, 0x26, 0x3B, 0x52, 0xC9, 0x85, 0x71, 0x49, 0xE3, 0xD8, 0x81, 0xE6, 0xBC, 0x86, 0x83, 0x35, 0xE7, 0x2B, 0x4C, 0xE8, 0xDE, 0xC7, 0x4E, 0x25, 0xE7, 0xBC, 0xAE },
			Tag = new byte[] { 0x4A, 0x57, 0xCE, 0x46, 0x75, 0x2, 0x27, 0xC8, 0x13, 0x9E, 0x12, 0x1D, 0x33, 0x63, 0x37, 0x78 },
		},
		new TestVectorAE
		{
			Key = new byte[] { 0xF0, 0xE, 0x4, 0x67, 0xDC, 0x51, 0x63, 0x9, 0xF6, 0x40, 0x24, 0x61, 0x59, 0x17, 0xDB, 0xC8 },
			IV = new byte[] { 0xC8, 0x22, 0xF9, 0xF4, 0xEF, 0xE7, 0x37, 0xCD },
			AAD = new byte[] { 0xEB, 0xA2, 0x38, 0x7B, 0xD, 0xF1, 0x69, 0xC4, 0xDB, 0xD1, 0x2D, 0x95, 0x58, 0xC9, 0x37, 0xDE, 0x86, 0x65, 0x36, 0xA3, 0x28, 0xE6, 0xB8, 0x51, 0xCC, 0x38, 0x48, 0x4E, 0x9, 0x24, 0xDA, 0xD3 },
			PlainText = new byte[] { 0x6, 0xC7, 0x6A, 0xFE, 0x3B, 0x43, 0xDF, 0x23, 0x7C, 0xB4, 0x35, 0x64, 0x33, 0x66, 0xDB, 0x81, 0xED, 0x45, 0x5A, 0xF1, 0x18, 0x94, 0x38, 0x31, 0xA, 0xE2, 0x1C, 0x5C, 0x46, 0x3A, 0x32, 0xFC, 0xFB, 0x9B, 0xD1, 0x1F, 0xCE, 0xA7, 0x5C, 0xAE, 0xD9, 0x9F, 0x66, 0xFB, 0xF4, 0x8F, 0x19, 0x99, 0x33, 0xC6, 0xD8, 0x70, 0xB0, 0x89, 0x23, 0x10, 0x8A, 0x9, 0xBB, 0x46, 0x2D, 0x92, 0x7, 0xED, 0xD4, 0x4, 0xB7, 0x7, 0x4D, 0x87, 0xA7, 0xD },
			CipherText = new byte[] { 0xDD, 0x2E, 0x7, 0xA2, 0xEA, 0x65, 0x80, 0x2E, 0x7B, 0xB2, 0xC, 0x5E, 0x81, 0xA6, 0x99, 0xD1, 0x33, 0x30, 0x83, 0xE6, 0xEC, 0x39, 0x6, 0x5A, 0xB2, 0x3B, 0x2A, 0xBF, 0xE3, 0x4, 0x54, 0xE3, 0xA8, 0x18, 0x47, 0xA5, 0x2B, 0xD5, 0x71, 0x42, 0x18, 0x1A, 0x64, 0xEB, 0xB, 0x6A, 0xFA, 0xAC, 0x5, 0xAD, 0x75, 0xC6, 0x2D, 0x49, 0x7D, 0xA3, 0x5, 0xAF, 0x35, 0xA7, 0xBF, 0xB, 0x8E, 0x5F, 0xA9, 0xFF, 0x15, 0x2, 0x78, 0xBA, 0x38, 0x23 },
			Tag = new byte[] { 0x60, 0x23, 0xA, 0xAA, 0x2C, 0x31, 0x71, 0xA2, 0x52, 0xD6, 0x7A, 0x22, 0xD5, 0x61, 0xA6, 0x67 },
		},
		new TestVectorAE
		{
			Key = new byte[] { 0xFC, 0x7B, 0x6, 0x83, 0x23, 0xAE, 0xB7, 0xCE, 0x61, 0xD2, 0xF7, 0x4A, 0x78, 0x2E, 0x41, 0x98 },
			IV = new byte[] { 0x53, 0x93, 0x6E, 0x35, 0x47, 0x92, 0x21, 0x51 },
			AAD = new byte[] { 0x99, 0xEC, 0x62, 0xDE, 0x40, 0x4A, 0x89, 0xE7, 0xED, 0x2E, 0x5E, 0x23, 0x49, 0x2A, 0x7E, 0xCA, 0x7D, 0x50, 0x1, 0xA7, 0xF8, 0xE9, 0x59, 0x6F, 0x6C, 0x6A, 0x7D, 0x51, 0x1A, 0xA3, 0x48, 0xCE, 0x50, 0x3A, 0x1B, 0xCB },
			PlainText = new byte[] { 0x30, 0x58, 0xCA, 0xE4, 0xB2, 0xA2, 0xB6, 0x4, 0xED, 0x6B, 0x36, 0x32, 0x92, 0x27, 0x3C, 0xAE, 0xB9, 0x23, 0x35, 0x3D, 0xE0, 0x74, 0x30, 0xC1, 0x30, 0x2E, 0x43, 0xC, 0x3, 0xD6, 0x2A, 0xF6, 0xA4, 0x19, 0x32, 0xAD, 0x55, 0xBD, 0x56, 0x4D, 0x97, 0xD9, 0xA7, 0xB1, 0xA5, 0x41, 0xF1, 0x88, 0x42, 0x45, 0x9B, 0xAB, 0xB4, 0x9B, 0xCF, 0xAE, 0x11, 0x99, 0xDB, 0xB4, 0xB9, 0xA7, 0xD7, 0x88, 0x24, 0xF5, 0x88, 0xEA, 0xDE, 0x69, 0x85, 0x27, 0xCF, 0xDC, 0x98, 0xED, 0xC0, 0x85, 0x67, 0x5C },
			CipherText = new byte[] { 0xD0, 0x1B, 0x10, 0x10, 0x12, 0xE, 0xA3, 0xAD, 0xBF, 0x58, 0x15, 0x63, 0x3C, 0x71, 0x72, 0xEE, 0xCF, 0x1D, 0x79, 0x66, 0x5B, 0x93, 0xE5, 0xE5, 0xFA, 0x74, 0x73, 0x6E, 0x38, 0x95, 0xEB, 0x4A, 0x33, 0x9B, 0x74, 0xB9, 0x65, 0xE3, 0x5E, 0xF8, 0x28, 0xA8, 0xEC, 0x82, 0x2A, 0x7F, 0xB0, 0xBD, 0x8D, 0xF3, 0xA1, 0x67, 0x42, 0x2B, 0xDE, 0x34, 0xC0, 0x22, 0xCB, 0x2E, 0x5D, 0x45, 0x8C, 0xDD, 0xD2, 0xC6, 0x7E, 0x4E, 0xFE, 0xB9, 0x75, 0xCE, 0x2A, 0xA3, 0xD9, 0xB2, 0x5B, 0x37, 0x46, 0xE1 },
			Tag = new byte[] { 0xC9, 0x10, 0x5C, 0x6C, 0x26, 0x76, 0xBC, 0x52, 0x6C, 0xC2, 0xB5, 0xA7, 0x31, 0x5A, 0x44, 0x34 },
		}
	};

		[Fact]
		public void LEA256_CCM_Encryption_AllTestVectorsPassing()
		{
			for (var i = 0; i < Lea256CcmTestVectors.Length; i++)
			{
				// Arrange
				var testvector = Lea256CcmTestVectors[i];
				var cipher = new Symmetric.LEA.CCM();

				// Act
				cipher.Init(Mode.ENCRYPT, testvector.Key, testvector.IV, testvector.Tag.Length);
				cipher.UpdateAAD(testvector.AAD);
				ReadOnlySpan<byte> actual = cipher.DoFinal(testvector.PlainText);
				var tag = actual.Slice(actual.Length - 16, 16);
				var cipherText = actual[..^16];

				// Assert
				Assert.True(cipherText.SequenceEqual(testvector.CipherText), "LEA-256-CCM encryption ciphertext test case #" + (i + 1));
				Assert.True(tag.SequenceEqual(testvector.Tag), "LEA-256-CCM encryption tag test case #" + (i + 1));
			}
		}

		[Fact]
		public void LEA256_CCM_Decryption_AllTestVectorsPassing()
		{
			for (var i = 0; i < Lea256CcmTestVectors.Length; i++)
			{
				// Arrange
				var testvector = Lea256CcmTestVectors[i];
				var cipher = new Symmetric.LEA.CCM();

				// Act
				cipher.Init(Mode.DECRYPT, testvector.Key, testvector.IV, testvector.Tag.Length);
				cipher.UpdateAAD(testvector.AAD);
				var aggregated = new byte[testvector.CipherText.Length + testvector.Tag.Length];
				testvector.CipherText.CopyTo(aggregated, 0);
				testvector.Tag.CopyTo(aggregated, testvector.CipherText.Length);
				ReadOnlySpan<byte> actual = cipher.DoFinal(aggregated);

				// Assert
				Assert.True(actual.SequenceEqual(testvector.PlainText), "LEA-256-CCM decryption test case #" + (i + 1));
			}
		}

		[Fact]
		public void LEA192_CCM_Encryption_AllTestVectorsPassing()
		{
			for (var i = 0; i < Lea192CcmTestVectors.Length; i++)
			{
				// Arrange
				var testvector = Lea192CcmTestVectors[i];
				var cipher = new Symmetric.LEA.CCM();

				// Act
				cipher.Init(Mode.ENCRYPT, testvector.Key, testvector.IV, testvector.Tag.Length);
				cipher.UpdateAAD(testvector.AAD);
				ReadOnlySpan<byte> actual = cipher.DoFinal(testvector.PlainText);
				var tag = actual.Slice(actual.Length - 16, 16);
				var cipherText = actual[..^16];

				// Assert
				Assert.True(cipherText.SequenceEqual(testvector.CipherText), "LEA-192-CCM encryption ciphertext test case #" + (i + 1));
				Assert.True(tag.SequenceEqual(testvector.Tag), "LEA-192-CCM encryption tag test case #" + (i + 1));
			}
		}

		[Fact]
		public void LEA192_CCM_Decryption_AllTestVectorsPassing()
		{
			for (var i = 0; i < Lea192CcmTestVectors.Length; i++)
			{
				// Arrange
				var testvector = Lea192CcmTestVectors[i];
				var cipher = new Symmetric.LEA.CCM();

				// Act
				cipher.Init(Mode.DECRYPT, testvector.Key, testvector.IV, testvector.Tag.Length);
				cipher.UpdateAAD(testvector.AAD);
				var aggregated = new byte[testvector.CipherText.Length + testvector.Tag.Length];
				testvector.CipherText.CopyTo(aggregated, 0);
				testvector.Tag.CopyTo(aggregated, testvector.CipherText.Length);
				ReadOnlySpan<byte> actual = cipher.DoFinal(aggregated);

				// Assert
				Assert.True(actual.SequenceEqual(testvector.PlainText), "LEA-192-CCM decryption test case #" + (i + 1));
			}
		}

		[Fact]
		public void LEA128_CCM_Encryption_AllTestVectorsPassing()
		{
			for (var i = 0; i < Lea128CcmTestVectors.Length; i++)
			{
				// Arrange
				var testvector = Lea128CcmTestVectors[i];
				var cipher = new Symmetric.LEA.CCM();

				// Act
				cipher.Init(Mode.ENCRYPT, testvector.Key, testvector.IV, testvector.Tag.Length);
				cipher.UpdateAAD(testvector.AAD);
				ReadOnlySpan<byte> actual = cipher.DoFinal(testvector.PlainText);
				var tag = actual.Slice(actual.Length - 16, 16);
				var cipherText = actual[..^16];

				// Assert
				Assert.True(cipherText.SequenceEqual(testvector.CipherText), "LEA-128-CCM encryption ciphertext test case #" + (i + 1));
				Assert.True(tag.SequenceEqual(testvector.Tag), "LEA-128-CCM encryption tag test case #" + (i + 1));
			}
		}

		[Fact]
		public void LEA128_CCM_Decryption_AllTestVectorsPassing()
		{
			for (var i = 0; i < Lea128CcmTestVectors.Length; i++)
			{
				// Arrange
				var testvector = Lea128CcmTestVectors[i];
				var cipher = new Symmetric.LEA.CCM();

				// Act
				cipher.Init(Mode.DECRYPT, testvector.Key, testvector.IV, testvector.Tag.Length);
				cipher.UpdateAAD(testvector.AAD);
				var aggregated = new byte[testvector.CipherText.Length + testvector.Tag.Length];
				testvector.CipherText.CopyTo(aggregated, 0);
				testvector.Tag.CopyTo(aggregated, testvector.CipherText.Length);
				ReadOnlySpan<byte> actual = cipher.DoFinal(aggregated);

				// Assert
				Assert.True(actual.SequenceEqual(testvector.PlainText), "LEA-128-CCM decryption test case #" + (i + 1));
			}
		}
	}
}