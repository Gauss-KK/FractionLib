
Imports System
Imports System.Numerics
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks

Imports knumerics

REM  //////////////////////////////////////////////////////
REM  // Filename: UsingMyFractionVB-001.vb
REM  //
REM  //      Get  FractionLib.dll at:  https:://github.com/Gauss-KK/FractiobLib
REM  //
REM  //       Require: NET Framework 4.8
REM  //
REM  //
REM  // Compile: vbc UsingMyFractionVB-001.vb /r:System.Numerics.dll /r:FractionLib.dll
REM  // Execute: UsingMyFractionVB-001
REM  // Output:
REM  //     Let
REM  //        a1 = 17/2
REM  //        a2 = 6/5
REM  //
REM  //     Then we get
REM  //        a1 + a2 = 97/10
REM  //        a1 - a2 = 73/10
REM  //        a1 * a2 = 51/5
REM  //        a1 / a2 = 85/12
REM  //        a1 % a2 = 1/10
REM  //        -a1 = -17/2
REM  // 
REM  //    Press any key...
REM  //
REM  //   Date: 2022.07.13
REM  //////////////////////////////////////////////////////
	


Namespace UseMyFraction
    Module Program
        Sub Main(args As String())
            Dim a1 As New MyFraction(17, 2)
            Dim a2 As New MyFraction(120, 100)
            REM Console.WriteLine("a1 = {0}", a1)
            
            Console.WriteLine("Let")
            Console.WriteLine("  a1 = {0}", a1)
            Console.WriteLine("  a2 = {0}", a2)
            Console.WriteLine()

           Console.WriteLine("Then we get")
            Console.WriteLine("  a1 + a2 = {0}", a1 + a2)
            Console.WriteLine("  a1 - a2 = {0}", a1 - a2)
            Console.WriteLine("  a1 * a2 = {0}", a1 * a2)
            Console.WriteLine("  a1 / a2 = {0}", a1 / a2)
            Console.WriteLine("  a1 % a2 = {0}", a1 Mod a2)
            Console.WriteLine("  -a1 = {0}", -a1)
            Console.WriteLine()

            Console.Write("Press any key...")
            Console.ReadKey()
        End Sub

    End Module
End Namespace
