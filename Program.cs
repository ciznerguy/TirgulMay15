using System;

namespace TirgulMay15
{
    internal class Program
    {
        public class Table
        {
            private int id;     // מספר השולחן
            private int seats;  // מספר הסועדים המקסימלי
            private int state;  // 0: פנוי, 1: מוזמן, 2: תפוס

            public Table(int id, int seats)
            {
                this.id = id;
                this.seats = seats;
                this.state = 0;
            }

            public int Book(int n)
            {
                if (state != 0 || seats < n)
                    return -1;

                state = 1;
                return id;
            }

            public int UnBook()
            {
                if (state == 2)
                    return -1;

                state = 0;
                return id;
            }

            // גישה לתכונות (נדרש למסעדה)
            public int GetSeats()
            {
                return seats;
            }

            public int GetState()
            {
                return state;
            }
        }

        public class Restaurant
        {
            private Table[] tables;

            public Restaurant(Table[] tables)
            {
                this.tables = tables;
            }

            public int BookTable(int numGuests)
            {
                // שלב א: בדיקה ראשונה – סופרים כמה שולחנות מתאימים
                int count = 0;
                for (int i = 0; i < tables.Length; i++)
                {
                    if (tables[i].Book(numGuests) != -1)
                    {
                        tables[i].UnBook();
                        count++;
                    }
                }

                // אם אין שולחנות מתאימים – החזר -1
                if (count == 0)
                    return -1;

                // שלב ב: יצירת מערך באורך מדויק לשולחנות הפנויים
                Table[] candidates = new Table[count];
                int index = 0;

                // שלב ג: ניסיון מחדש, הפעם לצורך שמירת הפניות למתאימים
                for (int i = 0; i < tables.Length; i++)
                {
                    if (tables[i].Book(numGuests) != -1)
                    {
                        tables[i].UnBook();
                        candidates[index] = tables[i];
                        index++;
                    }
                }

                // שלב ד: בחירה אקראית והזמנה סופית
                Random rnd = new Random();
                int chosenIndex = rnd.Next(candidates.Length);
                return candidates[chosenIndex].Book(numGuests);
            }

        }

        static void Main(string[] args)
        {
            // הדגמה קצרה
            Table[] tables = new Table[]
            {
                new Table(1, 2),
                new Table(2, 4),
                new Table(3, 6)
            };

            Restaurant r = new Restaurant(tables);
            int result = r.BookTable(3);
            Console.WriteLine("Table booked: " + result);
        }
    }
}
