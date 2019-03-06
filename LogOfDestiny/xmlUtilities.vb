Imports System.Xml


Module xmlUtilities

    Dim configFile As String
    Dim thisConfig As String

    Function determineConfig()

        Dim myDataDirectory = Application.UserAppDataPath

        Try
            ''COMPILE PATHS
            If frmLogEntry.configBL9A.Checked Then

                configFile = myDataDirectory & "\AMOD_CG_config.xml"
                'configFile = "C:\Program Files\LogOfDestiny\AMOD_CG_config.xml"  ''USE FOR MEIT
                'configFile = "C:\Documents and Settings\ktooley\My Documents\Visual Studio 2010\Projects\LogOfDestiny\LogOfDestiny\AMOD_CG_config.xml"

            ElseIf frmLogEntry.configBL9C.Checked Then

                configFile = myDataDirectory & "\AMOD_DDG_config.xml"
                'configFile = "C:\Program Files\LogOfDestiny\AMOD_DDG_config.xml"  ''USE FOR MEIT
                'configFile = "C:\Documents and Settings\ktooley\My Documents\Visual Studio 2010\Projects\LogOfDestiny\LogOfDestiny\AMOD_DDG_config.xml"

            ElseIf frmLogEntry.configBL9D.Checked Then

                configFile = myDataDirectory & "\AMOD_9D_config.xml"
                'configFile = "C:\Program Files\LogOfDestiny\AMOD_9D_config.xml"  ''USE FOR MEIT
                'configFile = "C:\Documents and Settings\ktooley\My Documents\Visual Studio 2010\Projects\LogOfDestiny\LogOfDestiny\AMOD_9D_config.xml"

            ElseIf frmLogEntry.configF105.Checked Then

                configFile = myDataDirectory & "\F105_config.xml"
                'configFile = "C:\Program Files\LogOfDestiny\F105_config.xml"  ''USE FOR MEIT
                'configFile = "C:\Documents and Settings\ktooley\My Documents\Visual Studio 2010\Projects\LogOfDestiny\LogOfDestiny\F105_config.xml"

            ElseIf frmLogEntry.configAWD.Checked Then

                configFile = myDataDirectory & "\AWD_config.xml"
                'configFile = "C:\Program Files\LogOfDestiny\AWD_config.xml"  ''USE FOR MEIT
                'configFile = "C:\Documents and Settings\ktooley\My Documents\Visual Studio 2010\Projects\LogOfDestiny\LogOfDestiny\AWD_config.xml"

            End If

        Catch ex As Exception

            MsgBox("A program error has occurred in determineConfig(): " & ex.Message)

        End Try

        Return configFile

    End Function

    Sub readXmlConfig()

        thisConfig = determineConfig()

        Dim xmlDoc = XDocument.Load(thisConfig)
        ReDim frmLogEntry.elementXmlDataArray(xmlDoc.Descendants("element").Count - 1, 12)
        ReDim frmLogEntry.elementArray(xmlDoc.Descendants("element").Count - 1)

        'Debug.Print(xmlDoc.Descendants("element").Count)


        Try

            Dim i = 0

            For Each element In xmlDoc.Descendants("element")

                frmLogEntry.elementXmlDataArray(i, 0) = element.Descendants("elementName").Value
                frmLogEntry.elementXmlDataArray(i, 1) = element.Descendants("shortName").Value
                frmLogEntry.elementXmlDataArray(i, 2) = element.Descendants("VCell").Value
                frmLogEntry.elementXmlDataArray(i, 3) = element.Descendants("elementHome").Value
                frmLogEntry.elementXmlDataArray(i, 4) = element.Descendants("startCell").Value
                frmLogEntry.elementXmlDataArray(i, 5) = element.Descendants("groupBox").Value
                'frmLogEntry.elementXmlDataArray(i, 6) = element.Descendants("upButton").Value
                'frmLogEntry.elementXmlDataArray(i, 7) = element.Descendants("dgButton").Value
                'frmLogEntry.elementXmlDataArray(i, 8) = element.Descendants("dnButton").Value
                'frmLogEntry.elementXmlDataArray(i, 9) = element.Descendants("naButton").Value
                frmLogEntry.elementXmlDataArray(i, 10) = element.Descendants("isComposite").Value
                frmLogEntry.elementXmlDataArray(i, 11) = element.Descendants("majorDependencies").Value
                frmLogEntry.elementXmlDataArray(i, 12) = element.Descendants("minorDependencies").Value


                'Debug.Print("*************")
                'Debug.Print(frmLogEntry.elementXmlDataArray(i, 0))
                'Debug.Print(frmLogEntry.elementXmlDataArray(i, 1))
                'Debug.Print(frmLogEntry.elementXmlDataArray(i, 2))
                'Debug.Print(frmLogEntry.elementXmlDataArray(i, 3))
                'Debug.Print(frmLogEntry.elementXmlDataArray(i, 4))

                frmLogEntry.elementArray(i) = New CElement With {.elementName = frmLogEntry.elementXmlDataArray(i, 0), _
                                                                 .shortName = frmLogEntry.elementXmlDataArray(i, 1), _
                                                                 .VCell = frmLogEntry.elementXmlDataArray(i, 2), _
                                                                 .elementHome = frmLogEntry.elementXmlDataArray(i, 3), _
                                                                 .startCell = frmLogEntry.elementXmlDataArray(i, 4), _
                                                                 .groupBox = frmLogEntry.elementXmlDataArray(i, 5), _
                                                                 .isComposite = frmLogEntry.elementXmlDataArray(i, 10), _
                                                                 .majorDependencies = frmLogEntry.elementXmlDataArray(i, 11), _
                                                                 .minorDependencies = frmLogEntry.elementXmlDataArray(i, 12)}

                '.upButton = frmLogEntry.elementXmlDataArray(i, 6), _
                '.dgButton = frmLogEntry.elementXmlDataArray(i, 7), _
                '.dnButton = frmLogEntry.elementXmlDataArray(i, 8), _
                '.naButton = frmLogEntry.elementXmlDataArray(i, 9), _

                i = i + 1

            Next

        Catch ex As Exception

            MsgBox("A program error has occurred in readXmlFile::createArrays: " & ex.Message)

        End Try

        Try

            For Each gb As GroupBox In frmLogEntry.TabPage1.Controls.OfType(Of GroupBox)()

                For Each element As CElement In frmLogEntry.elementArray

                    If element.groupBox = gb.Name Then

                        gb.Text = element.shortName

                        'Add Element Buttons

                        If element.shortName = "ORTS Detect" Then

                        Else

                            element.upButton.Location = New Point(4, 17)

                            element.dgButton.Location = New Point(42, 17)

                            element.dnButton.Location = New Point(95, 17)

                            element.naButton.Location = New Point(150, 17)

                            gb.Controls.Add(element.upButton)
                            gb.Controls.Add(element.dgButton)
                            gb.Controls.Add(element.dnButton)
                            gb.Controls.Add(element.naButton)

                        End If

                    End If

                Next

            Next

            For Each gb As GroupBox In frmLogEntry.TabPage1.Controls.OfType(Of GroupBox)()

                If gb.Text = "" Then

                    gb.Visible = False

                End If

            Next

        Catch ex As Exception

            MsgBox("A program error has occurred in readXmlFile::setGbxNames: " & ex.Message)

        End Try

    End Sub

End Module
