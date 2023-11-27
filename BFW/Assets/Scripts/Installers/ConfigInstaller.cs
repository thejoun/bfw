using Core;
using Nethereum.Hex.HexTypes;
using Zenject;

namespace Installers
{
    public class ConfigInstaller
    {
        public static void InstallInto(DiContainer container)
        {
            container.Bind<HexBigInteger>().WithId(ID.GasLimit).FromInstance(new HexBigInteger(10000000));
            container.Bind<float>().WithId(ID.EventFetchTimeInterval).FromInstance(1f);
        }
    }
}