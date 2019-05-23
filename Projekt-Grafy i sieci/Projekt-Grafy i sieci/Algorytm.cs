using System;

namespace algorytm2
{
    class Algorytm
    {
        public class Wierzcholek
        {
            public int dist { get; set; } //dystans
            public int prev { get; set; } //poprzedniks
            public bool odwiedzony { get; set; }

            public Wierzcholek()
            {

            }
        };

        private static int ZnajdzMinDist(Wierzcholek[] tab)
        {
            int min = -1;
            int mindist = 2147483647;
            for (int i = 0; i < tab.Length; i++)
            {
                if (tab[i].odwiedzony == false && tab[i].dist < mindist)
                {
                    min = i;
                    mindist = tab[i].dist;
                }
            }
            return min;
        }

        public static int Dijkstry(int[,] macierz, int startowy)
        {
            //przygotowanie macierzy, dla wierzch startowego dist = 0
            int iloscW = macierz.GetLength(0);
            Wierzcholek[] tab = new Wierzcholek[iloscW];
            for (int i = 0; i < iloscW; i++)
            {
                if (i == startowy)
                {
                    tab[i].dist = 0;
                    tab[i].odwiedzony = false;
                    tab[i].prev = -1;
                }
                else
                {
                    tab[i].dist = 2147483647; //zamiast nieskonczonosci
                    tab[i].odwiedzony = false;
                    tab[i].prev = -1;
                }
            }

            int u = startowy;
            while (u != -1)
            {
                tab[u].odwiedzony = true;
                for (int i = 0; i < iloscW; i++)
                {
                    if (macierz[u, i] > 0)
                        if (tab[u].dist + macierz[u, i] < tab[i].dist)
                        {
                            {
                                tab[i].dist = tab[u].dist + macierz[u, i];
                                tab[i].prev = u;
                            }
                        }
                }
                u = ZnajdzMinDist(tab);
                
            }
            Wierzcholek wynik = tab[0];
            int nazwWierzcholka = 0;
            for (int i = 1; i < tab.Length; i++)
            {
                if(tab[i].dist>wynik.dist)
                {
                    wynik = tab[i];
                    nazwWierzcholka = i;
                }
            }
            return nazwWierzcholka;
        }
    }
}
