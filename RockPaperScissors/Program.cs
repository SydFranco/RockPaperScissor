using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    class Program
    {
        public char[] PossiveisValores = { 'p', 'P', 's', 'S', 'R', 'r' };
        static void Main(string[] args)
        {
            Jogada vencedor = new Jogada();
            List<Jogada> torneio = new List<Jogada>();
            torneio.Add(new Jogada() { Jogador = "Syd", Escolha = 'P' });
            torneio.Add(new Jogada() { Jogador = "Victor", Escolha = 'S' });
            torneio.Add(new Jogada() { Jogador = "Dave", Escolha = 'P' });
            torneio.Add(new Jogada() { Jogador = "Wesley", Escolha = 'r' });
            torneio.Add(new Jogada() { Jogador = "Ronaldo", Escolha = 's' });
            torneio.Add(new Jogada() { Jogador = "Messi", Escolha = 'p' });
            torneio.Add(new Jogada() { Jogador = "CR7", Escolha = 'P' });
            torneio.Add(new Jogada() { Jogador = "Tomaz", Escolha = 'R' });

            if (torneio.Count % 2 != 0)
                throw new Exception("Número de jogadores do torneio incorreto.");
            else
            {
                try
                {
                    vencedor = new Program().Pps_tournament_winner(torneio);
                    Console.WriteLine(String.Format("O vencedor foi {0} com a jogada {1}", vencedor.Jogador, vencedor.Escolha));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public Jogada Pps_tournament_winner(List<Jogada> jogadasTorneio)
        {
            Jogada vencedorTorneio = new Jogada();
            Jogada vencedorRodada = new Jogada();
            List<Jogada> proximaEtapa = new List<Jogada>();
            List<Jogada> rodada = new List<Jogada>();
            int totalJogos = jogadasTorneio.Count / 2;

            while (jogadasTorneio.Count > 0)
            {
                rodada.Clear();
                Jogada jogada1 = jogadasTorneio.First();
                rodada.Add(jogada1);
                jogadasTorneio.Remove(jogadasTorneio.First());
                Jogada jogada2 = jogadasTorneio.First();
                rodada.Add(jogada2);
                jogadasTorneio.Remove(jogadasTorneio.First());

                proximaEtapa.Add(new Program().Rps_game_winner(rodada));
            }

            if (proximaEtapa.Count > 1)
                return Pps_tournament_winner(proximaEtapa);
            else
                vencedorTorneio = proximaEtapa.FirstOrDefault();

            return vencedorTorneio;
        }

        public Jogada Rps_game_winner(List<Jogada> rodada)
        {
            Jogada jogadaVencedora = new Jogada();

            if (rodada.Count != 2)
                throw new Exception(new WrongNumberOfPlayersError().message);
            else
            {
                foreach (char escolha in rodada.Select(x => x.Escolha))
                {
                    if (!PossiveisValores.Contains(escolha))
                        throw new Exception(new NoSuchStrategyError().message);
                }
                Jogada jogada1 = rodada.First();
                Jogada jogada2 = rodada.Last();
                if (new Rock().jogada.Contains(jogada1.Escolha))
                {
                    if (new Rock().venceDe.Contains(jogada2.Escolha))
                        jogadaVencedora = jogada1;
                    else
                        jogadaVencedora = jogada2;
                }
                else if (new Paper().jogada.Contains(jogada1.Escolha))
                {
                    if (new Paper().venceDe.Contains(jogada2.Escolha))
                        jogadaVencedora = jogada1;
                    else
                        jogadaVencedora = jogada2;
                }
                else if (new Scissor().jogada.Contains(jogada1.Escolha))
                {
                    if (new Scissor().venceDe.Contains(jogada2.Escolha))
                        jogadaVencedora = jogada1;
                    else
                        jogadaVencedora = jogada2;
                }

                return jogadaVencedora;
            }
        }
    }

    public class Jogada
    {
        public string Jogador { get; set; }
        public char Escolha { get; set; }
    }

    public abstract class A
    {
        public char[] jogada { get; set; }
        public char[] venceDe { get; set; }
    }

    public class Rock : A
    {
        public Rock()
        {
            jogada = new char[] { 'r', 'R'};
            venceDe = new char[] { 'r', 'R', 's', 'S' };
        }
    }

    public class Paper : A
    {
        public Paper()
        {
            jogada = new char[] { 'p', 'P' };
            venceDe = new char[] { 'p', 'P', 'r', 'R' };
        }
    }

    public class Scissor : A
    {
        public Scissor()
        {
            jogada = new char[] { 's', 'S' };
            venceDe = new char[] { 's', 'S', 'p', 'P' };
        }
    }

    public class WrongNumberOfPlayersError
    {
        public string message = "Número de jogadores não suportado.";
    }

    public class NoSuchStrategyError
    {
        public string message = "Estratégia de jogo não conhecida";
    }
}
