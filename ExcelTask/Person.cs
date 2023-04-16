namespace ExcelTask
{
    internal class Person
    {
        public int Age { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string PhoneNumber { get; set; }

        public Person(int age, string name, string surname, string phoneNumber)
        {
            Age = age;
            Name = name;
            Surname = surname;
            PhoneNumber = phoneNumber;
        }
    }
}
