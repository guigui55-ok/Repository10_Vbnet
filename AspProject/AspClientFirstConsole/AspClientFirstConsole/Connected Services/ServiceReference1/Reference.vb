﻿'------------------------------------------------------------------------------
' <auto-generated>
'     このコードはツールによって生成されました。
'     ランタイム バージョン:4.0.30319.42000
'
'     このファイルへの変更は、以下の状況下で不正な動作の原因になったり、
'     コードが再生成されるときに損失したりします。
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On


Namespace ServiceReference1
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ServiceModel.ServiceContractAttribute([Namespace]:="", ConfigurationName:="ServiceReference1.ICalc")>  _
    Public Interface ICalc
        
        <System.ServiceModel.OperationContractAttribute(Action:="urn:ICalc/Add", ReplyAction:="urn:ICalc/AddResponse")>  _
        Function Add(ByVal i As Integer, ByVal j As Integer) As Integer
        
        <System.ServiceModel.OperationContractAttribute(Action:="urn:ICalc/Add", ReplyAction:="urn:ICalc/AddResponse")>  _
        Function AddAsync(ByVal i As Integer, ByVal j As Integer) As System.Threading.Tasks.Task(Of Integer)
    End Interface
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")>  _
    Public Interface ICalcChannel
        Inherits ServiceReference1.ICalc, System.ServiceModel.IClientChannel
    End Interface
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")>  _
    Partial Public Class CalcClient
        Inherits System.ServiceModel.ClientBase(Of ServiceReference1.ICalc)
        Implements ServiceReference1.ICalc
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal endpointConfigurationName As String)
            MyBase.New(endpointConfigurationName)
        End Sub
        
        Public Sub New(ByVal endpointConfigurationName As String, ByVal remoteAddress As String)
            MyBase.New(endpointConfigurationName, remoteAddress)
        End Sub
        
        Public Sub New(ByVal endpointConfigurationName As String, ByVal remoteAddress As System.ServiceModel.EndpointAddress)
            MyBase.New(endpointConfigurationName, remoteAddress)
        End Sub
        
        Public Sub New(ByVal binding As System.ServiceModel.Channels.Binding, ByVal remoteAddress As System.ServiceModel.EndpointAddress)
            MyBase.New(binding, remoteAddress)
        End Sub
        
        Public Function Add(ByVal i As Integer, ByVal j As Integer) As Integer Implements ServiceReference1.ICalc.Add
            Return MyBase.Channel.Add(i, j)
        End Function
        
        Public Function AddAsync(ByVal i As Integer, ByVal j As Integer) As System.Threading.Tasks.Task(Of Integer) Implements ServiceReference1.ICalc.AddAsync
            Return MyBase.Channel.AddAsync(i, j)
        End Function
    End Class
End Namespace
