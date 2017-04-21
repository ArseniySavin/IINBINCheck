This solution may checking our individual tax number, using official government algorithm. 
If number was checked true. We will be getting true value.
<h1>How to test</h1> 
<ul>
  <li>Create console application;</li>
  <li>Add reference from <a href="https://www.nuget.org/packages/IINBINCheck">IINBINCheck</a>;</li>
  <li>Paste this code into Main method;</li>
</ul>

```csharp
  Console.WriteLine("Enter IIN/BIN:");
  string iin = Console.ReadLine();

  IinBinCheckContext contextIinChek = new IinBinCheckContext(iin, new DirectChek());
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
  
  // Data of parsing result IIN
  IINBINData iinData = contextIinChek.IINData;
  // or if you want get data the BIN
  IINBINData binData = contextIinChek.BINData
  
  Console.ReadKey();
```
