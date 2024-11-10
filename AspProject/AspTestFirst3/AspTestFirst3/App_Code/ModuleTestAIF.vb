
'※参照設定の追加が必要
Imports System.ServiceModel
Imports System.ServiceModel.Activation

<ServiceContract(Namespace:="")>
Public Interface ICalc
    <OperationContract()>
    Function Add(i As Integer, j As Integer) As Integer
End Interface



'######################
'Namespace MyMath
'    Public Class TestService
'        <ServiceContract()>
'        Public Interface ICalc
'            <OperationContract()>
'            Function Add(i As Integer, j As Integer) As Integer
'        End Interface

'End Namespace

'######################

'Module ModuleTestAIF
'    <ServiceContract()>
'    Public Interface ICalcModule
'        <OperationContract()>
'        Function Add(i As Integer, j As Integer) As Integer
'    End Interface


'End Module