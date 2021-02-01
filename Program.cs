using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Net;


namespace estudo_26_01
{
    class Program
    {
        static void Main(string[] args)
        {
            var html = "";

            using (WebClient web = new WebClient())
            {
                Encoding iso = Encoding.GetEncoding("ISO-8859-1");
                web.Encoding = iso;
                html = web.DownloadString("http://esaj.tjrn.jus.br/cpo/pg/show.do?processo.codigo=2Y000370T0000&processo.foro=106");
            }
            //Número do processo
            Regex regex_processo = new Regex(@"\d{7}-?\d{2}.\d{4}.\d{1}.\d{2}.\d{5}" , RegexOptions.Singleline | RegexOptions.IgnoreCase);
            var match_processo = regex_processo.Matches(html);
            //------------

            //Status do processo
            Regex regex_status = new Regex("<span style=\"color: red;\\s*\"\\s*>(.*?)</span>\\s*</td>\\s*</tr>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            var match_status = regex_status.Matches(html);
            //------------

            //Classe do processo
            Regex regex_classe = new Regex("<span id=\"\">(.*?)</span>\\s*</span>\\s*</td>\\s*</tr>\\s*</table>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            var match_classe = regex_classe.Matches(html);
            //------------

            //Área do processo
            Regex regex_area = new Regex("<span class=\"labelClass\">Área:</span> (.*?) \\s*</td>\\s*</tr>\\s*</table>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            var match_area = regex_area.Matches(html);
            //------------

            //Assunto do processo
            Regex regex_assunto = new Regex("<td valign=\"\">\\s*<span id=\"\">(Títulos de Crédito)</span>\\s*</td>\\s*</tr>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            var match_assunto = regex_assunto.Matches(html);
            //------------

            //Distribuição
            Regex regex_distribuicao = new Regex("<span id=\"\">(Sorteio - 28/09/2020 às 09:25)</span>\\s*</td>\\s*</tr>\\s*<tr>\\s*<td id=\"\" width=\"150\" valign=\"\">\\s*</td>\\s*<td valign=\"\">\\s*<span id=\"\">(Centro Judiciário de Solução de Conflitos e Cidadania - Mossoró)</span>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            var match_distribuicao = regex_distribuicao.Matches(html);
            //------------

            //buscar o n° do processo
            if(match_processo.Count > 0)
            {
                Console.WriteLine("Número do processo :" + match_processo[0].Value);
            }
            //-------------

            //buscar o status do processo
            if(match_status.Count > 0)
            {
                Console.WriteLine("Status :" + match_status[0].Groups[1].Value);
            }
            //------------

            //classe do processo
            if(match_classe.Count > 0)
            {
                Console.WriteLine(match_classe[0].Groups[1].Value);
            }
            //------------

            //Area 
            if(match_area.Count > 0)
            {
                Console.WriteLine(match_area[0].Groups[1].Value);
            }
            //------------

            //Assunto do processo
            if(match_assunto.Count > 0)
            {
                Console.WriteLine(match_assunto[0].Groups[1].Value);
            }
            //------------

            //Distribuição
            if(match_distribuicao.Count > 0)
            {
                Console.WriteLine(match_distribuicao[0].Groups[1].Value);
            }
            Console.ReadKey();


        }
    }
}
