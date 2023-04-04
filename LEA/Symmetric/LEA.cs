using System.Diagnostics;
using LEA.engine;
using LEA.OperatingMode;
using LEA.Macs;

namespace LEA.Symmetric
{
	public class Lea
	{
		private Lea() => Debug.Assert(true);

		public static BlockCipher GetEngine() => new LeaEngine();

		public sealed class Ecb : EcbMode
		{
			public Ecb() : base(GetEngine())
			{
			}
		}

		public sealed class Cbc : CbcMode
		{
			public Cbc() : base(GetEngine())
			{
			}
		}

		public sealed class Ctr : CtrMode
		{
			public Ctr() : base(GetEngine())
			{
			}
		}

		public sealed class Cfb : CfbMode
		{
			public Cfb() : base(GetEngine())
			{
			}
		}

		public sealed class Ofb : OfbMode
		{
			public Ofb() : base(GetEngine())
			{
			}
		}

		public sealed class Ccm : CcmMode
		{
			public Ccm() : base(GetEngine())
			{
			}
		}

		public sealed class Gcm : GcmMode
		{
			public Gcm() : base(GetEngine())
			{
			}
		}

		public sealed class CMac : Macs.CMac
		{
			public CMac() : base(GetEngine())
			{
			}
		}
	}
}
