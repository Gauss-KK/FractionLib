//////////////////////////////////////////////////////
// Filename: UsingMyFractionClr-001.cpp
//
//      Get  FractionLib.dll at:  https:://github.com/Gauss-KK/FractiobLib
//
//       Require: NET Framework 4.8
//
//
// Compile: cl /clr  /utf-8 UsingMyFractionClr-001.cpp /r:System.Numerics.dll /r:FractionLib.dll
// Execute:UsingMyFractionClr-001
// Output:
//     Let
//        a1 = 17/2
//        a2 = 6/5
//
//     Then we get
//        a1 + a2 = 97/10
//        a1 - a2 = 73/10
//        a1 * a2 = 51/5
//        a1 / a2 = 85/12
//        a1 % a2 = 1/10
//        -a1 = -17/2
// 
//    Press any key...
//
//   Date: 2022.07.13
//////////////////////////////////////////////////////
	

/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
*/

#using "System.Numerics.dll"
#using "FractionLib.dll"
	
using namespace knumerics;


int main(cli::array<System::String ^> ^args)
{
            MyFraction ^a1 = gcnew MyFraction(17, 2);
            MyFraction ^a2 = gcnew MyFraction(120, 100);
            // Console.WriteLine("a1 = {0}", a1);
            
            System::Console::WriteLine(L"Let");
            System::Console::WriteLine(L"  a1 = {0}", a1);
            System::Console::WriteLine(L"  a2 = {0}", a2);
            System::Console::WriteLine();

            System::Console::WriteLine(L"Then we get");
            System::Console::WriteLine(L"  a1 + a2 = {0}", a1 + a2);
            System::Console::WriteLine(L"  a1 - a2 = {0}", a1 - a2);
            System::Console::WriteLine(L"  a1 * a2 = {0}", a1 * a2);
            System::Console::WriteLine(L"  a1 / a2 = {0}", a1 / a2);
            System::Console::WriteLine(L"  a1 % a2 = {0}", a1 % a2);
            System::Console::WriteLine(L"  -a1 = {0}", -a1);
            System::Console::WriteLine();

            System::Console::Write(L"Press any key...");
             System::Console::ReadKey();
             
             return 0;
}
