Imports System
Imports System.Xml

Module Program
    Sub Main(args As String())
        Dim doc As New XmlDocument()
        doc.Load("SampleTrxXMLTestLogFile.trx")

        Dim user_input As String = Console.ReadLine()

        Dim nodeList As XmlNodeList = doc.DocumentElement.ChildNodes

        Dim Std_error As String = ""

        For Each node As XmlNode In nodeList
            If node.Name = "Results" Then
                For Each node2 As XmlNode In node.ChildNodes
                    If node2.Name = "UnitTestResult" Then
                        For Each node3 As XmlNode In node2.ChildNodes
                            If node3.Name = "Output" Then
                                For Each node4 As XmlNode In node3.ChildNodes
                                    If node4.Name = "ErrorInfo" OrElse node4.Name = "StdErr" Then

                                        If node4.Name = "StdErr" Then
                                            Std_error = node4.InnerText.ToString.Substring(0, 99)
                                        End If

                                        For Each node5 As XmlNode In node4.ChildNodes
                                            If node5.Name = "Message" AndAlso node5.InnerText.ToString.Contains(user_input) Then
                                                Console.WriteLine("UnitTestResult Node ID : " + node2.Attributes("relativeResultsDirectory").Value)
                                                Console.WriteLine("Test Name : " + node2.Attributes("testName").Value)
                                                Console.WriteLine("Message : " + node5.InnerText)
                                                Console.WriteLine("Std Error : " + Std_error)

                                                Console.WriteLine()
                                                Console.WriteLine()
                                                Console.WriteLine()
                                            End If
                                        Next
                                    End If
                                Next
                            End If
                        Next
                    End If
                Next
            End If
        Next
    End Sub
End Module