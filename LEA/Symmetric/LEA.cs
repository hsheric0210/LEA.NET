using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using LEA.engine;
using LEA.mode;
using LEA.mac;

namespace LEA.symm
{
	public class LEA
	{
		private LEA()
		{
			Debug.Assert(true);
		}

		public static BlockCipher GetEngine()
		{
			return new LeaEngine();
		}

		public sealed class ECB : ECBMode
		{
			public ECB() : base(GetEngine())
			{
			}
		}

		public sealed class CBC : CBCMode
		{
			public CBC() : base(GetEngine())
			{
			}
		}

		public sealed class CTR : CTRMode
		{
			public CTR() : base(GetEngine())
			{
			}
		}

		public sealed class CFB : CFBMode
		{
			public CFB() : base(GetEngine())
			{
			}
		}

		public sealed class OFB : OFBMode
		{
			public OFB() : base(GetEngine())
			{
			}
		}

		public sealed class CCM : CCMMode
		{
			public CCM() : base(GetEngine())
			{
			}
		}

		public sealed class GCM : GCMMode
		{
			public GCM() : base(GetEngine())
			{
			}
		}

		public sealed class CMAC : CMac
		{
			public CMAC() : base(GetEngine())
			{
			}
		}
	}
}