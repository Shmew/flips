﻿module Flips.Domain.UnitsOfMeasure

open Flips

//let inline private retype (x: 'T) : 'U = (# "" x: 'U #) 
//let inline private stripUoM (x: '``T<'M>``) =
//    let _ = x * (LanguagePrimitives.GenericOne : 'T)
//    retype x :'T


type Scalar<[<Measure>] 'Measure> = 
    | Value of UoM:float<'Measure> * Scalar:Domain.Scalar
with

    static member (+) (Value (_, lhsS):Scalar<'Measure>, Value (_, rhsS):Scalar<'Measure>) =
        let uom = FSharp.Core.LanguagePrimitives.FloatWithMeasure<'Measure> 1.0
        Value (uom, lhsS * rhsS)

    static member (+) (Value (_, s):Scalar<'Measure>, f:float<'Measure>) =
        let newF = float f
        let uom = FSharp.Core.LanguagePrimitives.FloatWithMeasure<'Measure> 1.0
        Value (uom, s + newF)

    static member (+) (f:float<'Measure>, s:Scalar<'Measure>) =
        s + f

    static member (*) (Value (uom, s):Scalar<_>, f) =
        let newUoM = uom * f
        let unitlessF = stripUoM f
        Value (newUoM, s * unitlessF)

    static member (*) (f:float<_>, s:Scalar<_>) =
        s * f

    static member (*) (Value (lhsUoM, lhsS):Scalar<_>, Value (rhsUoM, rhsS):Scalar<_>) =
        let newUoM = lhsUoM * rhsUoM
        Value (newUoM, lhsS * rhsS)

    static member (-) (Value (lhsUoM, lhsS):Scalar<'Measure>, Value (rhsUoM, rhsS):Scalar<'Measure>) =
        Value (lhsUoM, lhsS - rhsS)

    static member (-) (Value (uom, s):Scalar<'Measure>, f:float<'Measure>) =
        let unitlessF = stripUoM f
        Value (uom, s - unitlessF)

    static member (-) (f:float<'Measure>, Value (uom, s):Scalar<'Measure>) =
        let unitlessF = stripUoM f
        Value (uom, unitlessF - s)

    static member (/) (f:float<_>, Value (uom, s):Scalar<_>) =
        let unitlessF = stripUoM f
        Value (f / uom, unitlessF / s)

    static member (/) (Value (uom, s):Scalar<_>, f:float<_>) =
        let unitlessF = stripUoM f
        Value (uom / f, s / unitlessF)

    static member (/) (Value (lhsUoM, lhsS):Scalar<_>, Value (rhsUoM, rhsS):Scalar<_>) =
        Value (lhsUoM / rhsUoM, lhsS / rhsS)

    static member inline Zero = Value (0.0<_>, Flips.Domain.Scalar.Zero)


type Decision<[<Measure>] 'Measure> =
    | Value of UoM:float<'Measure> * Domain.Decision
with

    static member inline (*) (Value (uom, d):Decision<'M1>, f:float<'M2>) =
        let newF = flaot f
        let expr = d * newF
        let exprUoM = uom * f
        LinearExpression.Value (exprUoM, expr)

    static member inline (*) (f:float<'Measure>, d:Decision<'Measure>) =
        d * f

    //static member (*) (decision:Decision<_>, scalar:Scalar<_>) =
    //    LinearExpression.OfDecision decision * scalar

    //static member (*) (scalar:Scalar, decision:Decision) =
    //    LinearExpression.OfDecision decision * scalar

    //static member (+) (decision:Decision, f:float) =
    //    LinearExpression.OfDecision decision + f

    //static member (+) (f:float, decision:Decision) =
    //    LinearExpression.OfDecision decision + f

    //static member (+) (scalar:Scalar, decision:Decision) =
    //    LinearExpression.OfScalar scalar + decision

    //static member (+) (decision:Decision, scalar:Scalar) =
    //    LinearExpression.OfDecision decision + scalar

    //static member (+) (decision:Decision, rightDecision:Decision) =
    //    LinearExpression.OfDecision decision + rightDecision

    //static member (+) (decision:Decision, expr:LinearExpression) =
    //    LinearExpression.OfDecision decision + expr

    //static member (-) (decision:Decision, f:float) =
    //    LinearExpression.OfDecision decision - f

    //static member (-) (f:float, decision:Decision) =
    //    LinearExpression.OfDecision decision - f

    //static member (-) (scalar:Scalar, decision:Decision) =
    //    LinearExpression.OfScalar scalar + (-1.0 * decision)

    //static member (-) (decision:Decision, scalar:Scalar) =
    //    LinearExpression.OfDecision decision + (-1.0 * scalar)

    //static member (-) (decision:Decision, rightDecision:Decision) =
    //    LinearExpression.OfDecision decision + (-1.0 * rightDecision)

    //static member (-) (decision:Decision, expr:LinearExpression) =
    //    LinearExpression.OfDecision decision + (-1.0 * expr)

    //static member (<==) (decision:Decision, f:float) =
    //    LinearExpression.OfDecision decision <== f

    //static member (<==) (decision:Decision, scalar:Scalar) =
    //    LinearExpression.OfDecision decision <== scalar

    //static member (<==) (decision:Decision, rhsDecision:Decision) =
    //    LinearExpression.OfDecision decision <== rhsDecision

    //static member (<==) (decision:Decision, expr:LinearExpression) =
    //    LinearExpression.OfDecision decision <== expr

    //static member (==) (decision:Decision, f:float) =
    //    LinearExpression.OfDecision decision == f

    //static member (==) (decision:Decision, scalar:Scalar) =
    //    LinearExpression.OfDecision decision == scalar

    //static member (==) (decision:Decision, rhsDecision:Decision) =
    //    LinearExpression.OfDecision decision == rhsDecision

    //static member (==) (decision:Decision, expr:LinearExpression) =
    //    LinearExpression.OfDecision decision == expr

    //static member (>==) (decision:Decision, f:float) =
    //    LinearExpression.OfDecision decision >== f

    //static member (>==) (decision:Decision, scalar:Scalar) =
    //    LinearExpression.OfDecision decision >== scalar

    //static member (>==) (decision:Decision, rhsDecision:Decision) =
    //    LinearExpression.OfDecision decision >== rhsDecision

    //static member (>==) (decision:Decision, expr:LinearExpression) =
    //    LinearExpression.OfDecision decision >== expr

and LinearExpression<[<Measure>] 'Measure> =
    | Value of UoM:float<'Measure> * Domain.LinearExpression
    with

        //static member OfFloat<'Measure> (f:float<'Measure>) =
        //    let planF = float f
        //    let expr = Domain.LinearExpression (Set.empty, Map.empty, Map.empty, Domain.Scalar.Value planF)
        //    let uom = FSharp.Core.LanguagePrimitives.FloatWithMeasure<'Measure> 1.0
        //    Value (uom, expr)

        //static member OfScalar<'Measure> (Scalar.Value (_, s):Scalar<'Measure>) =
        //    let expr = LinearExpression.OfScalar s
        //    let uom = FSharp.Core.LanguagePrimitives.FloatWithMeasure<'Measure> 1.0
        //    Value (uom, expr)

        //static member inline OfDecision (Decision.Value (_, d):Decision<'Measure>) : LinearExpression<'Measure> =
        //    let expr = LinearExpression.OfDecision d
        //    let uom = FSharp.Core.LanguagePrimitives.FloatWithMeasure<'Measure> 1.0
        //    Value (uom, expr)

        //static member GetDecisions (LinearExpression.Value (_, expr):LinearExpression<_>) =
        //    expr.Decisions
        //    |> Map.toList
        //    |> List.map snd
        //    |> Set.ofList

        static member inline Zero =
            let expr = LinearExpression (Set.empty, Map.empty, Map.empty, Scalar.Zero)
            LinearExpression.Value (0.0<_>, expr)

        static member (+) (LinearExpression.Value (_, lExpr):LinearExpression<'Measure>, LinearExpression.Value (_, rExpr):LinearExpression<'Measure>) =
            let newExpr = lExpr + rExpr
            let uom = FSharp.Core.LanguagePrimitives.FloatWithMeasure<'Measure> 1.0
            LinearExpression.Value (uom, newExpr)

        static member (+) (Value (_, expr):LinearExpression<'Measure>, f:float<'Measure>) =
            let newF = float f
            let uom = FSharp.Core.LanguagePrimitives.FloatWithMeasure<'Measure> 1.0
            Value (uom, expr + newF)

        static member (+) (f:float<'Measure>, expr:LinearExpression<'Measure>) =
            expr + f

        static member (+) (Value (_, expr):LinearExpression<'Measure>, Scalar.Value (_, s):Scalar<'Measure>) =
            let uom = FSharp.Core.LanguagePrimitives.FloatWithMeasure<'Measure> 1.0
            Value (uom, expr + s)

        static member (+) (s:Scalar<'Measure>, expr:LinearExpression<'Measure>) =
            expr + s

        static member (+) (Value (_, expr):LinearExpression<'Measure>, Decision.Value (_,d):Decision<'Measure>) =
            let uom = FSharp.Core.LanguagePrimitives.FloatWithMeasure<'Measure> 1.0
            Value (uom, expr + d)

        static member (+) (d:Decision<'Measure>, expr:LinearExpression<'Measure>) =
            expr + d

        static member (*) (Value (_, expr):LinearExpression<'Measure1>, f:float<'Measure2>) =
            let newF = float f
            let uom = FSharp.Core.LanguagePrimitives.FloatWithMeasure<'Measure1 'Measure2> 1.0
            Value (uom, expr * newF)

        static member (*) (f:float<'Measure1>, expr:LinearExpression<'Measure2>) =
            expr * f

        static member inline (*) (Value (_,expr):LinearExpression<'Measure1>, Scalar.Value (_, s):Scalar<'Measure2>) =
            let uom = FSharp.Core.LanguagePrimitives.FloatWithMeasure<'M1 'M2> 1.0
            Value (uom, expr * s)

        static member (*) (scalar:Scalar<'Measure1>, expr:LinearExpression<'Measure2>) =
            expr * scalar

        static member (-) (expr:LinearExpression<'Measure>, f:float<'Measure>) =
            expr + (-1.0 * f)

        static member (-) (f:float<'Measure>, expr:LinearExpression<'Measure>) =
            f + (-1.0 * expr)

        static member (-) (expr:LinearExpression<'Measure>, s:Scalar<'Measure>) =
            expr + (-1.0 * s)

        static member (-) (s:Scalar<'Measure>, expr:LinearExpression<'Measure>) =
            s + (-1.0 * expr)

        //static member (-) (expr:LinearExpression<'Measure>, d:Decision<'Measure>) =
        //    expr + (-1.0 * d)

        //static member (-) (d:Decision, expr:LinearExpression) =
        //    d + (-1.0 * expr)

        //static member (-) (lExpr:LinearExpression, rExpr:LinearExpression) =
        //    lExpr + (-1.0 * rExpr)

        //static member (<==) (lhs:LinearExpression, rhs:float) =
        //    Inequality (lhs, LessOrEqual, LinearExpression.OfFloat rhs)

        //static member (<==) (lhs:LinearExpression, rhs:Scalar) =
        //    Inequality (lhs, LessOrEqual, LinearExpression.OfScalar rhs)

        //static member (<==) (lhs:LinearExpression, rhs:Decision) =
        //    Inequality (lhs, LessOrEqual, LinearExpression.OfDecision rhs)

        //static member (<==) (lhs:LinearExpression, rhs:LinearExpression) =
        //    Inequality (lhs, LessOrEqual, rhs)

        //static member (==) (lhs:LinearExpression, rhs:float) =
        //    Equality (lhs, LinearExpression.OfFloat rhs)

        //static member (==) (lhs:LinearExpression, rhs:Scalar) =
        //    Equality (lhs, LinearExpression.OfScalar rhs)

        //static member (==) (lhs:LinearExpression, rhs:Decision) =
        //    Equality (lhs, LinearExpression.OfDecision rhs)

        //static member (==) (lhs:LinearExpression, rhs:LinearExpression) =
        //    Equality (lhs, rhs)

        //static member (>==) (lhs:LinearExpression, rhs:float) =
        //    Inequality (lhs, GreaterOrEqual, LinearExpression.OfFloat rhs)

        //static member (>==) (lhs:LinearExpression, rhs:Scalar) =
        //    Inequality (lhs, GreaterOrEqual, LinearExpression.OfScalar rhs)

        //static member (>==) (lhs:LinearExpression, rhs:Decision) =
        //    Inequality (lhs, GreaterOrEqual, LinearExpression.OfDecision rhs)

        //static member (>==) (lhs:LinearExpression, rhs:LinearExpression) =
        //    Inequality (lhs, GreaterOrEqual, rhs)