using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MosParis
{
    class CCard
    {
        public string A { get; set; }
        public string B { get; set; }
        public bool del { get; set; }

        public CCard(string a, string b)
        {
            A = a;
            B = b;
            del = false;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            CCard[] cards = new CCard[10];
            cards[0] = new CCard("Кельн", "Москва");
            cards[1] = new CCard("Ямайка", "Токио");
            cards[2] = new CCard("Париж", "Рио");
            cards[3] = new CCard("Бостон", "Мельбурн");
            cards[4] = new CCard("Портленд", "Гавана");
            cards[5] = new CCard("Рио", "Оттава");
            cards[6] = new CCard("Оттава", "Портленд");
            cards[7] = new CCard("Москва", "Париж");
            cards[8] = new CCard("Гавана", "Ямайка");
            cards[9] = new CCard("Мельбурн", "Кельн");

            Console.WriteLine("---=== Исходный список ===----");
            foreach (var item in cards)
            {
                Console.WriteLine(item.A + " -> " + item.B);
            }
            Console.WriteLine();
            List<CCard> CardList = SortCards(cards);
            Console.WriteLine("---=== Упорядоченный список ===----");
            foreach (var item in CardList)
            {
                Console.WriteLine(item.A + " -> " + item.B);
            }
          
            Console.ReadLine();
        }

        static List<CCard> SortCards(CCard[] cards)
        {
           //так как из условия задачи следует, что гарантировано получится цепь, 
            //то каждый элемент цепи имеет определенное место относительно правого и левого элемента.
            // иными словами отдельно элемент не может иметь промежуточного значения, как в случае сортировки чисел
            //Таким образом, берем первый элемент и ищем карточку, следующую за ним 
            //и помещаем в новый массив (новый массив для избежания дополнительного цикла), 
            //если таковой нет, значит это последняя карточка и тогда
            //берем первый элемен в новом массиве и к нему ищем предыдущую карточку
            //Cложность алгоритма равна О(n*n-n)/2
            //Так как мы имеем перебор N-раз (в двух частях на на поиск хвоста и корня)
            //Внутренний цикл имеет N-1 итераций, так как один элемент мы извлекли в начале алгоритма
            //На внутреннем цикле при нахожении элемента происходит выход из цикла, то количество итераций уменьшается вдвое
            //можно было бы уменьшить сложность уделением карточки из исходного цикла, 
            //но уделение из массива требует больше ресурсов, нежели пометка del у элемента
            //На пракике я обычно использую linq для поиска в массиве.
            List<CCard> CardList = new List<CCard>();

            int cardsLength = cards.Length; //сохраняем в переменную, чтобы каждый раз не вызавать
            CCard card_temp = new CCard(cards[0].A, cards[0].B);
            CardList.Add(cards[0]); //добавляем первую карточку в новый массив, далее ищем следующую за ней 
            int d = 0;
            bool find = false;
            int Qnt = 0;
            while (d < cardsLength)
            {
                for (int i = 1; i < cardsLength; i++)
                {
                    if (!cards[i].del) //если в новый массив не перенесена, то сравниваем
                    if (card_temp.B == cards[i].A)
                    {
                        CardList.Add(cards[i]); //нашли, записываем
                        card_temp.A = cards[i].A; 
                        card_temp.B = cards[i].B;
                        cards[i].del = true;
                        find = true;
                        break;
                    }
                    Qnt++; //считаем количество итераций, для алгоритма значения не имеет
                }
                if (!find) //значит это хвост
                    break;
                d++;
                find = false;
            }
            //первый элемент из упорядоченного списка
            card_temp.A = CardList[0].A;
            card_temp.B = CardList[0].B;

            //ищем обратную подстановку (в начало списка)
            d = CardList.Count;
            while (d < cardsLength)
            {
                for (int i = 1; i < cardsLength; i++)
                {
                    if (!cards[i].del)
                        if (card_temp.A == cards[i].B)
                        {
                            CardList.Insert(0, cards[i]); //вставляем в начало списка
                            card_temp.A = cards[i].A;
                            card_temp.B = cards[i].B;
                            cards[i].del = true;
                            break;
                        }
                    Qnt++;
                }
                d++;
            }

            Console.WriteLine("Количество итераций: "+ Qnt.ToString());

            return CardList; //возвращаем готовый список

           
        }
    }
}

