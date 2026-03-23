using NivelStocareDate;
using System.Configuration;

namespace EvidentaEvenimente
{
    public static class StocareFactory
    {
        private const string FORMAT_SALVARE = "FormatSalvare";

        public static IStocareData GetAdministratorStocare()
        {
            string formatSalvare = ConfigurationManager.AppSettings[FORMAT_SALVARE] ?? "memorie";

            switch (formatSalvare.ToLower())
            {
                case "memorie":
                    return new AdministrareEvenimenteMemorie();
                default:
                    return new AdministrareEvenimenteMemorie();
            }
        }
    }
}