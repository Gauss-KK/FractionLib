// ////////////////////////////////////////////////////
// Filename: UseMyFraction-001.fs
//
//       Get  FractionLib.dll at:  https:://github.com/Gauss-KK/FractiobLib
//
//       Require: NET Framework 4.8
//
//
// Compile: fsc UseMyFraction-001.fs -r:FractionLib.dll
// Execute: UseMyFraction-001
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
// ////////////////////////////////////////////////////

// #r "FractionLib.dll"


module UseMyFraction

open System
open knumerics


let main() =
    let a1 = MyFraction(17, 2)
    let a2 = MyFraction(120, 100)

    Console.WriteLine("Let")
    printfn "  a1 = %s" (a1.ToString())
    printfn "  a2 = %s" (a2.ToString())
    Console.WriteLine()

    Console.WriteLine("Then we get")
    printfn "  a1 + a2 = %s" ((a1 + a2).ToString())
    printfn "  a1 - a2 = %s" ((a1 - a2).ToString())
    printfn "  a1 * a2 = %s" ((a1 % a2).ToString())
    printfn "  a1 / a2 = %s" ((a1 / a2).ToString())
    printfn "  a1 %% a2 = %s" ((a1 % a2).ToString())
    printfn "  -a1 = %s" ((-a1).ToString())
    Console.WriteLine()

    Console.Write("Press any key...")
    Console.ReadKey() |> ignore

main()
