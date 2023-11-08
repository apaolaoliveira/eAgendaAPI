using System.Text.RegularExpressions;

namespace eAgenda.WebApi.Filtros.StringExtension
{
    public static class StringExtension
    {
        public static string SepararPalavrasPorMaiusculas(this string nomeMetodo)
        {
            string padraoParaSepararPorMaiusculas = @"([A-Z][a-z]*)";

            MatchCollection matches = Regex.Matches(nomeMetodo, padraoParaSepararPorMaiusculas);

            string nomeMetodoSeparado = "";

            foreach (Match m in matches)
            {
                nomeMetodoSeparado += m.Value + " ";
            }

            return nomeMetodoSeparado;
        }
    }
}
