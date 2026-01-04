using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace مشروع_الخوارزميات
{
    // تعريف التقديرات
    enum Grade { Fail, Good, VeryGood, Excellent }

    // كلاس الطالب
    class Student
    {
        public int Id;
        public string Name;
        public string City;
        public double Test1, Test2, Average;
        public Grade StudentGrade;

        // روابط اللائحة المزدوجة
        public Student Next;
        public Student Prev;

        // مشيد لتعيين القيم وحساب المحصلة تلقائياً
        public Student(int id, string name, string city, double t1, double t2, Grade g)
        {
            Id = id;
            Name = name;
            City = city;
            Test1 = t1;
            Test2 = t2;
            Average = (t1 + t2) / 2; // حساب المحصلة
            StudentGrade = g;
        }
    }

    // كلاس اللائحة المزدوجة للطلاب
    class StudentList
    {
        public Student Head; // بداية اللائحة
        public Student Tail; // نهاية اللائحة

        // دالة لإضافة طالب في بداية اللائحة
        public void AddFirst(Student newStudent)
        {
            if (Head == null)
            {
                Head = Tail = newStudent;
            }
            else
            {
                newStudent.Next = Head;
                Head.Prev = newStudent;
                Head = newStudent;
            }
        }

        // دالة لإضافة طالب في نهاية اللائحة
        public void AddLast(Student newStudent)
        {
            if (Tail == null)
            {
                Head = Tail = newStudent;
            }
            else
            {
                Tail.Next = newStudent;
                newStudent.Prev = Tail;
                Tail = newStudent;
            }
        }

        // دالة مساعدة خاصة لإنشاء طالب من خلال إدخال المستخدم
        private Student CreateStudentFromConsole()
        {
            Console.Write("أدخل رقم الطالب: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("أدخل اسم الطالب: ");
            string name = Console.ReadLine();

            Console.Write("أدخل مدينة الطالب: ");
            string city = Console.ReadLine();

            Console.Write("أدخل علامة الاختبار الأول: ");
            double t1 = double.Parse(Console.ReadLine());

            Console.Write("أدخل علامة الاختبار الثاني: ");
            double t2 = double.Parse(Console.ReadLine());

            // يمكن تحديد التقدير بناءً على المعدل هنا إذا أردت
            // كمثال، نضع التقدير "جيد" بشكل افتراضي
            Grade g = Grade.Good;

            return new Student(id, name, city, t1, t2, g);
        }

        // 1. دالة لإضافة طالب من الإدخال (تضيف للنهاية تلقائياً)
        public void AddFromConsole()
        {
            Student newStudent = CreateStudentFromConsole();
            AddLast(newStudent);
        }

        // 2. دالة لإضافة طالب يدوياً في البداية
        public void AddFirstManually()
        {
            Console.WriteLine("\n--- إضافة طالب جديد في بداية اللائحة ---");
            Student newStudent = CreateStudentFromConsole();
            AddFirst(newStudent);
            Console.WriteLine("تمت إضافة الطالب بنجاح.");
        }

        // 3. دالة لإضافة طالب يدوياً في النهاية
        public void AddLastManually()
        {
            Console.WriteLine("\n--- إضافة طالب جديد في نهاية اللائحة ---");
            Student newStudent = CreateStudentFromConsole();
            AddLast(newStudent);
            Console.WriteLine("تمت إضافة الطالب بنجاح.");
        }

        // 4. دالة لعرض جميع الطلاب في اللائحة
        public void DisplayAll()
        {
            Console.WriteLine("\n--- عرض جميع الطلاب ---");
            Student current = Head;
            if (current == null)
            {
                Console.WriteLine("اللائحة فارغة.");
                return;
            }
            while (current != null)
            {
                Console.WriteLine($"ID: {current.Id}, Name: {current.Name}, City: {current.City}, Average: {current.Average}, Grade: {current.StudentGrade}");
                current = current.Next;
            }
        }

        // 5. دالة لفرز الطلاب حسب الاسم أو المحصلة
        public void Sort(string sortType)
        {
            if (Head == null || Head.Next == null) return; // لا حاجة للفرز إذا كانت اللائحة فارغة أو بها عنصر واحد

            // تحويل اللائحة المزدوجة إلى قائمة عادية لسهولة الفرز
            List<Student> tempList = new List<Student>();
            Student current = Head;
            while (current != null)
            {
                tempList.Add(current);
                current = current.Next;
            }

            // الفرز حسب اختيار المستخدم
            if (sortType == "1") // فرز بالاسم
            {
                tempList.Sort((s1, s2) => s1.Name.CompareTo(s2.Name));
                Console.WriteLine("\nتم فرز الطلاب حسب الاسم.");
            }
            else // فرز بالمحصلة
            {
                tempList.Sort((s1, s2) => s1.Average.CompareTo(s2.Average));
                Console.WriteLine("\nتم فرز الطلاب حسب المحصلة.");
            }

            // إعادة بناء اللائحة المزدوجة بعد الفرز
            Head = null;
            Tail = null;
            foreach (var studentNode in tempList)
            {
                // إعادة تعيين المؤشرات قبل الإضافة لتجنب المشاكل
                studentNode.Next = null;
                studentNode.Prev = null;
                AddLast(studentNode);
            }

            DisplayAll(); // عرض اللائحة بعد الفرز
        }

        // دالة البحث العودي عن علامة محددة
        public void RecursiveSearch(Student current, double targetMark)
        {
            if (current == null) return; // قاعدة التوقف: وصلنا لنهاية اللائحة

            if (current.Average == targetMark)
            {
                Console.WriteLine($"-> تم العثور على الطالب: {current.Name} (المحصلة: {current.Average})");
            }

            // استدعاء الدالة لنفسها مع الطالب التالي
            RecursiveSearch(current.Next, targetMark);
        }
    }

    // الكلاس الرئيسي للبرنامج
    internal class Program
    {
        static void Main(string[] args)
        {
            // إنشاء كائن من الكلاس المسؤول عن اللائحة
            StudentList list = new StudentList();

            // إدخال بيانات 5 طلاب كبداية كما هو مطلوب في المسألة
            Console.WriteLine("--- نظام إدارة طلاب مادة الخوارزميات ---");
            for (int i = 1; i <= 5; i++)
            {
                Console.WriteLine($"\nإدخال بيانات الطالب رقم {i}:");
                list.AddFromConsole(); // دالة مساعدة تأخذ البيانات من المستخدم وتضيفها
            }

            bool running = true;
            while (running)
            {
                // عرض القائمة للمستخدم
                Console.WriteLine("\n--- القائمة الرئيسية ---");
                Console.WriteLine("1. إضافة طالب جديد (بداية/نهاية)");
                Console.WriteLine("2. فرز الطلاب (حسب الاسم أو المحصلة)");
                Console.WriteLine("3. البحث عن علامة محددة (بحث عودي)");
                Console.WriteLine("4. عرض جميع الطلاب");
                Console.WriteLine("5. خروج");
                Console.Write("اختر رقم العملية: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Console.WriteLine("1. إضافة في البداية\n2. إضافة في النهاية");
                        Console.Write("اختر الموقع: ");
                        string pos = Console.ReadLine();
                        if (pos == "1")
                            list.AddFirstManually();
                        else
                            list.AddLastManually();
                        break;

                    case "2":
                        Console.WriteLine("1. فرز بالاسم (A-Z)\n2. فرز بالمحصلة (من الأدنى للأعلى)");
                        Console.Write("اختر نوع الفرز: ");
                        string sortType = Console.ReadLine();
                        if (sortType == "1" || sortType == "2")
                            list.Sort(sortType);
                        else
                            Console.WriteLine("خيار فرز خاطئ.");
                        break;

                    case "3":
                        Console.Write("أدخل العلامة التي تبحث عنها: ");
                        double target = double.Parse(Console.ReadLine());
                        Console.WriteLine($"\nنتائج البحث عن المحصلة {target}:");
                        list.RecursiveSearch(list.Head, target);
                        break;

                    case "4":
                        list.DisplayAll();
                        break;

                    case "5":
                        running = false;
                        break;

                    default:
                        Console.WriteLine("خيار خاطئ، حاول مرة أخرى.");
                        break;

                }
            }
        }
    }
}