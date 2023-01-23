using LEA.Engine;
using LEA.Mac;
using LEA.Mode;

namespace LEA.Symmetric;

public static class Lea
{
    public static BlockCipher GetEngine() => new LeaEngine();

    /// <summary>
    /// Electronic CodeBook mode
    /// </summary>
    public sealed class Ecb : ECBMode
    {
        public Ecb() : base(GetEngine())
        {
        }
    }

    /// <summary>
    /// Cipher Block Chaining mode
    /// </summary>
    public sealed class Cbc : CbcMode
    {
        public Cbc() : base(GetEngine())
        {
        }
    }

    /// <summary>
    /// Counter mode
    /// </summary>
    public sealed class Ctr : CTRMode
    {
        public Ctr() : base(GetEngine())
        {
        }
    }

    /// <summary>
    /// Cipher FeedBack mode
    /// </summary>
    public sealed class Cfb : CFBMode
    {
        public Cfb() : base(GetEngine())
        {
        }
    }

    /// <summary>
    /// Output FeedBack mode
    /// </summary>
    public sealed class Ofb : OfbMode
    {
        public Ofb() : base(GetEngine())
        {
        }
    }

    /// <summary>
    /// Counter with CBC-MAC mode
    /// </summary>
    public sealed class Ccm : CcmMode
    {
        public Ccm() : base(GetEngine())
        {
        }
    }

    /// <summary>
    /// Galois Counter Mode
    /// </summary>
    public sealed class Gcm : GCMMode
    {
        public Gcm() : base(GetEngine())
        {
        }
    }

    /// <summary>
    /// CBC-MAC mode
    /// </summary>
    public sealed class Cmac : CMac
    {
        public Cmac() : base(GetEngine())
        {
        }
    }
}