Module Config

    Sub dependents()

        For Each compositeElement As CElement In frmLogEntry.elementArray

            Dim isUp As Boolean = False
            Dim isDegraded As Boolean = False
            Dim isDown As Boolean = False

            If compositeElement.isComposite = "true" Then

                'Debug.Print("****" + compositeElement.shortName + "****")
                'Debug.Print(compositeElement.dependencies)

                Dim splitMajorString() As String
                Dim splitMinorString() As String

                splitMajorString = Split(compositeElement.majorDependencies, ",")
                splitMinorString = Split(compositeElement.minorDependencies, ",")

                If compositeElement.shortName = "AWS" Then

                    'Debug.Print("Skipping...this is AWS")

                Else

                    For intX = 0 To UBound(splitMinorString)
                        'Debug.Print(splitMinorString(intX))
                        If getComponentStatus(splitMinorString(intX)) = "D" Then

                            setCompositeStatus(compositeElement.shortName, "DG")
                            setCompositeDgrdButton(compositeElement.shortName)
                            Exit For

                        End If

                    Next

                End If

                ''ADDED for AWS Composite Status-->
                If compositeElement.shortName = "AWS" Then
                    'Debug.Print("Entering AWS processing")

                    ''NEW ARRAY
                    Dim thisStatusArray() As String

                    thisStatusArray = Split(compositeElement.majorDependencies, ",")

                    For intX = 0 To UBound(splitMajorString)

                        thisStatusArray(intX) = getComponentStatus(splitMajorString(intX))

                    Next

                    For Each statusString As String In thisStatusArray

                        'Debug.Print(statusString)
                        If statusString = "D" Then

                            'Debug.Print("WE HAVE A DOWN STATUS")
                            isDown = True
                            Exit For

                        End If

                    Next

                    For Each statusString As String In thisStatusArray

                        'Debug.Print(statusString)
                        If statusString = "DG" Then

                            'Debug.Print("WE HAVE A DGRD STATUS")
                            isDegraded = True
                            Exit For

                        End If

                    Next

                    For Each statusString As String In thisStatusArray

                        'Debug.Print(statusString)
                        If statusString = "U" Then

                            'Debug.Print("WE HAVE A UP STATUS")
                            isUp = True
                            Exit For

                        End If

                    Next

                    If isDown = True Then

                        setCompositeAwsStatus(compositeElement.shortName, "D")

                    ElseIf isDegraded = True Then

                        setCompositeAwsStatus(compositeElement.shortName, "DG")

                    ElseIf isUp = True Then

                        setCompositeAwsStatus(compositeElement.shortName, "U")

                    End If


                Else
                    ''-->END ADDED for AWS Composite Status

                    For intX = 0 To UBound(splitMajorString)
                        'Debug.Print(splitMajorString(intX))
                        If getComponentStatus(splitMajorString(intX)) = "D" Then

                            setCompositeStatus(compositeElement.shortName, "D")
                            setCompositeDownButton(compositeElement.shortName)
                            Exit For

                        ElseIf getComponentStatus(splitMajorString(intX)) = "DG" Then

                            setCompositeStatus(compositeElement.shortName, "DG")
                            setCompositeDgrdButton(compositeElement.shortName)

                        End If

                    Next

                End If ''ADDED for AWS Composite Status

            End If

        Next

    End Sub

    Sub setCompositeStatus(ByVal itsShortName, ByVal itsStatus)

        Try

            For Each element As CElement In frmLogEntry.elementArray

                If element.shortName = itsShortName Then

                    If element.getStatus() = "U" Or element.getStatus() = "DG" Then

                        element.setStatus(element.shortName, itsStatus)
                        'Debug.Print("Set " + findCompositeElement.shortName + " to DOWN based on dependency status")

                    End If

                    Exit For

                End If

            Next

        Catch ex As Exception

            MsgBox("A program error has occurred in setCompositeStatus: " & ex.Message)

        End Try

    End Sub

    Sub setCompositeAwsStatus(ByVal itsShortName, ByVal itsStatus)
        ''ADDED for AWS Composite Status-->

        Try

            For Each element As CElement In frmLogEntry.elementArray

                If element.shortName = itsShortName Then

                    element.setStatus(element.shortName, itsStatus)

                    Exit For

                End If

            Next

        Catch ex As Exception

            MsgBox("A program error has occurred in setCompositeAwsStatus: " & ex.Message)

        End Try
        ''-->END ADDED for AWS Composite Status
    End Sub

    Function getComponentStatus(ByVal itsShortName)

        Dim itsStatus = ""

        Try

            For Each element As CElement In frmLogEntry.elementArray

                If element.shortName = itsShortName Then

                    itsStatus = element.getStatus()

                    Exit For

                End If

            Next

        Catch ex As Exception

            MsgBox("A program error has occurred in getComponentStatus: " & ex.Message)

        End Try

        Return itsStatus

    End Function

    Sub setCompositeDownButton(ByVal itsShortName)

        Dim itsButton = ""

        Try

            For Each element As CElement In frmLogEntry.elementArray

                If element.shortName = itsShortName Then

                    element.dnButton.Checked = True

                    'itsButton = findButton.dnButton

                    'For Each gb As GroupBox In frmLogEntry.TabPage1.Controls.OfType(Of GroupBox)()

                    '    For Each rb As RadioButton In gb.Controls.OfType(Of RadioButton)()

                    '        If rb.Name = itsButton Then

                    '            'rb.PerformClick()
                    '            rb.Checked = True

                    '        End If

                    '    Next

                    'Next

                End If

            Next

        Catch ex As Exception

            MsgBox("A program error has occurred in setCompositeDownButton: " & ex.Message)

        End Try

    End Sub

    Sub setCompositeDgrdButton(ByVal itsShortName)

        Dim itsButton = ""

        Try

            For Each element As CElement In frmLogEntry.elementArray

                If element.shortName = itsShortName Then

                    element.dgButton.Checked = True

                    'itsButton = findButton.dgButton

                    'For Each gb As GroupBox In frmLogEntry.TabPage1.Controls.OfType(Of GroupBox)()

                    '    For Each rb As RadioButton In gb.Controls.OfType(Of RadioButton)()

                    '        If rb.Name = itsButton Then

                    '            'rb.PerformClick()
                    '            rb.Checked = True

                    '        End If

                    '    Next

                    'Next

                End If

            Next

        Catch ex As Exception

            MsgBox("A program error has occurred in setCompositeDgrdButton: " & ex.Message)

        End Try

    End Sub

End Module