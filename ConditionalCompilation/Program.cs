const int x = 1;

#region Boxing
static void Method1()
{
    Console.WriteLine("Result: {0}", x);
}
#endregion

#region NotBoxing
static void Method2()
{
    Console.WriteLine("Result: {0}", x.ToString());
}
#endregion

#if DEBUG
Method1();
#else
Method2();
#endif