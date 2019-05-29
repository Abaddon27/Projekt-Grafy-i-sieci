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
        };

        private static int ZnajdzMinDist(Wierzcholek[] tab)
        {
            int min = -1;
            int mindist = 2147483647;
            for (int i = 0; i < tab.Length; i++)
            {
                if (tab[i].odwiedzony == false && tab[i].dist < mindist) //zwracamy wierzcholek, jesli go jeszcze nie badalismy i ma najmniejszy dist
                {
                    min = i;
                    mindist = tab[i].dist;
                }
            }
            return min;
        }

        public static int Dijkstry(int[,] macierz, int startowy) //zwraca sumę najmniejszych odleglosci od danego wierzcholka do wszystkich pozostałych
        {
            //przygotowanie macierzy, dla wierzch startowego dist = 0
            int iloscW = macierz.GetLength(0);
            Wierzcholek[] tab = new Wierzcholek[iloscW];


            for (int i = 0; i < iloscW; i++)
            {
                if (i == startowy)
                {
                    Wierzcholek w = new Wierzcholek();
                    w.dist = 0;
                    w.odwiedzony = false;
                    w.prev = -1;
                    tab[i] = w;
                }
                else
                {
                    Wierzcholek w = new Wierzcholek();
                    w.dist = 21474836;
                    w.odwiedzony = false;
                    w.prev = -1;
                    tab[i] = w;
                }
            }

            //ZACZYNAMY NASZ ALGORYTM :D
            int u = startowy; //nasze poszukiwania zaczniemy od badań względem wierzch. wskazanego przez uzytkownika.
            while (u != -1) //pozniej u będzie się zmieniać, gdy wszystkie będą odiwedzone met. pom. "Znajdz Min Dist wypluje -1";
            {
                tab[u].odwiedzony = true; //zapamiętajmy, że już wzgledem niego badalismy
                for (int i = 0; i < iloscW; i++)
                {
                    if (macierz[u, i] > 0) //jesli nie ma krawedzi miedzy wierzcholkami to nie ma co szukac, tam bedzie "-1"
                        if (tab[u].dist + macierz[u, i] < tab[i].dist) //jesli znaleziony dist jest mniejszy od zapamietanego to zamieniamy
                        {
                            {
                                tab[i].dist = tab[u].dist + macierz[u, i];
                                tab[i].prev = u;
                            }
                        }
                }
                u = ZnajdzMinDist(tab); //teraz bedziemy szukać względem kolejnego wierzchołka - o najmniejszym na razie dystansie
            }

            //sumujemy odleglosci
            int suma = 0;
            for (int i = 0; i < tab.Length; i++)
            {
                suma = suma + tab[i].dist;
            }
            return suma;
        }

        public static int BestPlace(int[,] macierz) //szukamy takiego wierzchołka, ktorego suma odleglosci do pozostałych jest najniejsza
        {
            int najlepszeMiejsce = -1;
            int najlepszaSuma = 21474836;
            for (int i = 0; i < macierz.GetLength(0); i++)
            {
                int suma = Dijkstry(macierz, i);
                if (suma < najlepszaSuma)
                {
                    najlepszeMiejsce = i;
                    najlepszaSuma = suma;
                }
            }
            return najlepszeMiejsce;
        }       
    }
}

