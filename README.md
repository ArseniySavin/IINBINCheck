This solution may checking our individual tax number, using official government algorithm. 
If number was checked true. We will be getting true value.

<h1>How to test</h1> 
Create console application. Add reference from [NuGet library](https://www.nuget.org/packages/IINBINCheck) and paste this code into Main method.
```csharp
  Console.WriteLine("Enter IIN/BIN:");
  string iin =Console.ReadLine();

  ContextIinCheck contextIinChek = new ContextIinCheck(iin, new DirectChek());
  contextIinChek.Check();

  Console.WriteLine("Direction check: " + contextIinChek.IsCheked.ToString());

  if(!contextIinChek.IsCheked)
  {
    contextIinChek.ContextChekAlgoritm = new IndirectChek();
    contextIinChek.Check();
    Console.WriteLine("Indirect check: " + contextIinChek.IsCheked.ToString());
  }
    
  Console.WriteLine("Curent key bit: " + contextIinChek.CurrentRank.ToString());

  Console.WriteLine("Result check: " + contextIinChek.IsCheked.ToString());
  
  // Data of parsing result  
  Data data = contextIinChek.IINBINData;
  
  Console.WriteLine(string.Format("{0} | {1} | {2} | {3} | {4} | {5} | {6} | {7}", 
      data.BirtDate, data.DocumentType, data.Gender, data.Rank, 
      data.RegistrationDate, data.SequenceNumber, data.SpecialCompanyType, data.Type));
    
  Console.ReadKey();
```
