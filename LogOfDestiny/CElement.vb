Imports Microsoft.Office.Interop.Excel
Imports Microsoft.Office.Core
Imports System.Threading

Public Class CElement

    Public VCell As Integer
    Public ev As Integer
    Public elementStatus As String
    Public elementHome As String
    Public startCell As String
    Public elementName As String
    Public shortName As String
    Public groupBox As String
    'Public upButton As String
    'Public dgButton As String
    'Public dnButton As String
    'Public naButton As String
    Public WithEvents upButton As New RadioButton() With {.Text = "UP", _
                                               .Anchor = (AnchorStyles.Top Or AnchorStyles.Left), _
                                               .Size = New Size(36, 16)}
    Public WithEvents dgButton As New RadioButton() With {.Text = "DGRD", _
                                              .Anchor = (AnchorStyles.Top Or AnchorStyles.Left), _
                                              .Size = New Size(51, 16)}
    Public WithEvents dnButton As New RadioButton() With {.Text = "DOWN", _
                                              .Anchor = (AnchorStyles.Top Or AnchorStyles.Left), _
                                              .Size = New Size(53, 16)}
    Public WithEvents naButton As New RadioButton() With {.Text = "NA", _
                                               .Anchor = (AnchorStyles.Top Or AnchorStyles.Left), _
                                               .Size = New Size(37, 16)}
    Public isComposite As String
    Public majorDependencies As String
    Public minorDependencies As String

    Private statusName As String
    Dim ElemSheet As Worksheet, ElemStart As Range

    Sub setStatus(ByVal elem, ByVal stat)

        Try

            statusName = elem
            elementStatus = stat

        Catch ex As Exception

            MsgBox("A program error has occurred in setStatus(): " & ex.Message)

        End Try

    End Sub

    Function getStatus()

        'Try

        '    getStatus = elementStatus

        'Catch ex As Exception

        '    MsgBox("A program error has occurred in getStatus(): " & ex.Message)

        'End Try

        getStatus = elementStatus

    End Function

    Sub calculateMetrics()

        Try

            ev = VCell - 1

            ElemSheet = frmLogEntry.oWB.Worksheets(elementHome)
            ElemStart = frmLogEntry.ws.Range(startCell)

            frmLogEntry.oWB.Application.ScreenUpdating = False

            frmLogEntry.ws.Activate()
            ElemStart.Activate()
            MAX(ElemSheet, ev)

            frmLogEntry.ws.Activate()
            ElemStart.Activate()
            MAXFR(ElemSheet, ev)

            frmLogEntry.ws.Activate()
            ElemStart.Activate()
            UP(ElemSheet, ev)

            frmLogEntry.ws.Activate()
            ElemStart.Activate()
            DGRD(ElemSheet, ev)

            frmLogEntry.ws.Activate()
            ElemStart.Activate()
            DOWN(ElemSheet, ev)

            frmLogEntry.ws.Activate()
            ElemStart.Activate()
            OFFLINE(ElemSheet, ev)

            frmLogEntry.ws.Activate()
            ElemStart.Activate()
            BREAK(ElemSheet, ev)

            frmLogEntry.oWB.Application.ScreenUpdating = True

        Catch ex As Exception

            MsgBox("A program error has occurred in calculateMetrics(): " & ex.Message)

        End Try

    End Sub

    Sub MAX(ByVal ElemSheet, ByVal ev)

        Try

            Dim Check, nr, nc

            Check = True

            Do

                Do Until Check = False

                    For nr = 3 To ElemSheet.Rows.Count

                        nc = 1

                        If frmLogEntry.oXL.ActiveCell.Value = "U" Or frmLogEntry.oXL.ActiveCell.Value = "DG" Then

                            If frmLogEntry.oXL.ActiveCell.Offset(-1, 0).Value = "D" Or frmLogEntry.oXL.ActiveCell.Offset(-1, 0).Value = "O" Then

                                ElemSheet.Cells(nr, nc).Value = "0"

                            ElseIf frmLogEntry.oXL.ActiveCell.Offset(-1, 0).Value = "B" Then

                                ElemSheet.Cells(nr, nc).Value = ElemSheet.Cells(nr - 1, nc).Value

                            Else : ElemSheet.Cells(nr, nc).Value = frmLogEntry.oXL.ActiveCell.Offset(0, -ev).Value - _
                                frmLogEntry.oXL.ActiveCell.Offset(-1, -ev).Value + ElemSheet.Cells(nr - 1, nc).Value

                            End If

                        ElseIf frmLogEntry.oXL.ActiveCell.Value = "B" Then

                            If frmLogEntry.oXL.ActiveCell.Offset(-1, 0).Value = "U" Or frmLogEntry.oXL.ActiveCell.Offset(-1, 0).Value = "DG" Then

                                ElemSheet.Cells(nr, nc).Value = frmLogEntry.oXL.ActiveCell.Offset(0, -ev).Value - _
                                    frmLogEntry.oXL.ActiveCell.Offset(-1, -ev).Value + ElemSheet.Cells(nr - 1, nc).Value

                            ElseIf frmLogEntry.oXL.ActiveCell.Offset(-1, 0).Value = "D" Or frmLogEntry.oXL.ActiveCell.Offset(-1, 0).Value = "O" Then

                                ElemSheet.Cells(nr, nc).Value = "0"

                            Else : ElemSheet.Cells(nr, nc).Value = ElemSheet.Cells(nr - 1, nc).Value

                            End If

                        ElseIf frmLogEntry.oXL.ActiveCell.Value = "D" Or frmLogEntry.oXL.ActiveCell.Value = "O" Then

                            If frmLogEntry.oXL.ActiveCell.Offset(-1, 0).Value = "D" Or frmLogEntry.oXL.ActiveCell.Offset(-1, 0).Value = "O" Then

                                ElemSheet.Cells(nr, nc).Value = "0"

                            ElseIf frmLogEntry.oXL.ActiveCell.Offset(-1, 0).Value = "B" Then

                                ElemSheet.Cells(nr, nc).Value = ElemSheet.Cells(nr - 1, nc).Value

                            Else : ElemSheet.Cells(nr, nc).Value = frmLogEntry.oXL.ActiveCell.Offset(0, -ev).Value - _
                                frmLogEntry.oXL.ActiveCell.Offset(-1, -ev).Value + ElemSheet.Cells(nr - 1, nc).Value

                            End If

                        Else : Check = False

                            Exit Do

                        End If

                        frmLogEntry.oXL.ActiveCell.Offset(1, 0).Activate()

                    Next nr

                Loop

            Loop Until Check = False

        Catch ex As Exception

            MsgBox("A program error has occurred in " + shortName + " MAX(): " & ex.Message)

        End Try

    End Sub

    Sub MAXFR(ByVal ElemSheet, ByVal ev)

        Try

            Dim Check, nr, nc

            Check = True

            Do

                Do Until Check = False

                    For nr = 3 To ElemSheet.Rows.Count

                        nc = 2

                        If frmLogEntry.oXL.ActiveCell.Value = "U" Then

                            If frmLogEntry.oXL.ActiveCell.Offset(-1, 0).Value = "D" Or _
                                frmLogEntry.oXL.ActiveCell.Offset(-1, 0).Value = "DG" Or _
                                frmLogEntry.oXL.ActiveCell.Offset(-1, 0).Value = "O" Then

                                ElemSheet.Cells(nr, nc).Value = "0"

                            ElseIf frmLogEntry.oXL.ActiveCell.Offset(-1, 0).Value = "B" Then

                                ElemSheet.Cells(nr, nc).Value = ElemSheet.Cells(nr - 1, nc).Value

                            Else : ElemSheet.Cells(nr, nc).Value = frmLogEntry.oXL.ActiveCell.Offset(0, -ev).Value - _
                                frmLogEntry.oXL.ActiveCell.Offset(-1, -ev).Value + ElemSheet.Cells(nr - 1, nc).Value

                            End If

                        ElseIf frmLogEntry.oXL.ActiveCell.Value = "B" Then

                            If frmLogEntry.oXL.ActiveCell.Offset(-1, 0).Value = "U" Then

                                ElemSheet.Cells(nr, nc).Value = frmLogEntry.oXL.ActiveCell.Offset(0, -ev).Value - _
                                    frmLogEntry.oXL.ActiveCell.Offset(-1, -ev).Value + ElemSheet.Cells(nr - 1, nc).Value

                            ElseIf frmLogEntry.oXL.ActiveCell.Offset(-1, 0).Value = "D" Or _
                                frmLogEntry.oXL.ActiveCell.Offset(-1, 0).Value = "DG" Or _
                                frmLogEntry.oXL.ActiveCell.Offset(-1, 0).Value = "O" Then

                                ElemSheet.Cells(nr, nc).Value = "0"

                            Else : ElemSheet.Cells(nr, nc).Value = ElemSheet.Cells(nr - 1, nc).Value

                            End If

                        ElseIf frmLogEntry.oXL.ActiveCell.Value = "D" Or (frmLogEntry.oXL.ActiveCell.Value = "O") Or _
                            (frmLogEntry.oXL.ActiveCell.Value = "DG") Then

                            If frmLogEntry.oXL.ActiveCell.Offset(-1, 0).Value = "D" Or _
                                frmLogEntry.oXL.ActiveCell.Offset(-1, 0).Value = "DG" Or _
                                frmLogEntry.oXL.ActiveCell.Offset(-1, 0).Value = "O" Then

                                ElemSheet.Cells(nr, nc).Value = "0"

                            ElseIf frmLogEntry.oXL.ActiveCell.Offset(-1, 0).Value = "B" Then

                                ElemSheet.Cells(nr, nc).Value = ElemSheet.Cells(nr - 1, nc).Value

                            Else : ElemSheet.Cells(nr, nc).Value = frmLogEntry.oXL.ActiveCell.Offset(0, -ev).Value - _
                                frmLogEntry.oXL.ActiveCell.Offset(-1, -ev).Value + ElemSheet.Cells(nr - 1, nc).Value

                            End If

                        Else : Check = False

                            Exit Do

                        End If

                        frmLogEntry.oXL.ActiveCell.Offset(1, 0).Activate()

                    Next nr

                Loop

            Loop Until Check = False

        Catch ex As Exception

            MsgBox("A program error has occurred in " + shortName + " MAXFR(): " & ex.Message)

        End Try

    End Sub

    Sub UP(ByVal ElemSheet, ByVal ev)

        Try

            Dim Check, nr, nc

            Check = True

            Do

                Do Until Check = False

                    For nr = 3 To ElemSheet.Rows.Count

                        nc = 3

                        If frmLogEntry.oXL.ActiveCell.Value = "U" Then

                            If frmLogEntry.oXL.ActiveCell.Offset(-1, 0).Value = "D" Or _
                                frmLogEntry.oXL.ActiveCell.Offset(-1, 0).Value = "DG" Or _
                                frmLogEntry.oXL.ActiveCell.Offset(-1, 0).Value = "B" Or _
                                frmLogEntry.oXL.ActiveCell.Offset(-1, 0).Value = "O" Then

                                ElemSheet.Cells(nr, nc).Value = ElemSheet.Cells(nr - 1, nc).Value

                            Else : ElemSheet.Cells(nr, nc).Value = frmLogEntry.oXL.ActiveCell.Offset(0, -ev).Value - _
                                frmLogEntry.oXL.ActiveCell.Offset(-1, -ev).Value + ElemSheet.Cells(nr - 1, nc).Value

                            End If

                        ElseIf frmLogEntry.oXL.ActiveCell.Value = "B" Or frmLogEntry.oXL.ActiveCell.Value = "D" Or _
                            frmLogEntry.oXL.ActiveCell.Value = "O" Or frmLogEntry.oXL.ActiveCell.Value = "DG" Then

                            If frmLogEntry.oXL.ActiveCell.Offset(-1, 0).Value = "U" Then

                                ElemSheet.Cells(nr, nc).Value = frmLogEntry.oXL.ActiveCell.Offset(0, -ev).Value - _
                                    frmLogEntry.oXL.ActiveCell.Offset(-1, -ev).Value + ElemSheet.Cells(nr - 1, nc).Value

                            Else : ElemSheet.Cells(nr, nc).Value = ElemSheet.Cells(nr - 1, nc).Value

                            End If

                        Else : Check = False

                            Exit Do

                        End If

                        frmLogEntry.oXL.ActiveCell.Offset(1, 0).Activate()

                    Next nr

                Loop

            Loop Until Check = False

        Catch ex As Exception

            MsgBox("A program error has occurred in " + shortName + " UP(): " & ex.Message)

        End Try

    End Sub

    Sub DGRD(ByVal ElemSheet, ByVal ev)

        Try

            Dim Check, nr, nc

            Check = True

            Do

                Do Until Check = False

                    For nr = 3 To ElemSheet.Rows.Count

                        nc = 4

                        If frmLogEntry.oXL.ActiveCell.Value = "DG" Then

                            If frmLogEntry.oXL.ActiveCell.Offset(-1, 0).Value = "D" Or _
                                frmLogEntry.oXL.ActiveCell.Offset(-1, 0).Value = "U" Or _
                                frmLogEntry.oXL.ActiveCell.Offset(-1, 0).Value = "B" Or _
                                frmLogEntry.oXL.ActiveCell.Offset(-1, 0).Value = "O" Then

                                ElemSheet.Cells(nr, nc).Value = ElemSheet.Cells(nr - 1, nc).Value

                            Else : ElemSheet.Cells(nr, nc).Value = frmLogEntry.oXL.ActiveCell.Offset(0, -ev).Value - _
                                frmLogEntry.oXL.ActiveCell.Offset(-1, -ev).Value + ElemSheet.Cells(nr - 1, nc).Value

                            End If

                        ElseIf frmLogEntry.oXL.ActiveCell.Value = "B" Or frmLogEntry.oXL.ActiveCell.Value = "D" Or _
                            frmLogEntry.oXL.ActiveCell.Value = "O" Or frmLogEntry.oXL.ActiveCell.Value = "U" Then

                            If frmLogEntry.oXL.ActiveCell.Offset(-1, 0).Value = "DG" Then

                                ElemSheet.Cells(nr, nc).Value = frmLogEntry.oXL.ActiveCell.Offset(0, -ev).Value - _
                                    frmLogEntry.oXL.ActiveCell.Offset(-1, -ev).Value + ElemSheet.Cells(nr - 1, nc).Value

                            Else : ElemSheet.Cells(nr, nc).Value = ElemSheet.Cells(nr - 1, nc).Value

                            End If

                        Else : Check = False

                            Exit Do

                        End If

                        frmLogEntry.oXL.ActiveCell.Offset(1, 0).Activate()

                    Next nr

                Loop

            Loop Until Check = False

        Catch ex As Exception

            MsgBox("A program error has occurred in " + shortName + " DGRD(): " & ex.Message)

        End Try

    End Sub

    Sub DOWN(ByVal ElemSheet, ByVal ev)

        Try

            Dim Check, nr, nc

            Check = True

            Do

                Do Until Check = False

                    For nr = 3 To ElemSheet.Rows.Count

                        nc = 5

                        If frmLogEntry.oXL.ActiveCell.Value = "D" Then

                            If frmLogEntry.oXL.ActiveCell.Offset(-1, 0).Value = "U" Or _
                                frmLogEntry.oXL.ActiveCell.Offset(-1, 0).Value = "DG" Or _
                                frmLogEntry.oXL.ActiveCell.Offset(-1, 0).Value = "B" Or _
                                frmLogEntry.oXL.ActiveCell.Offset(-1, 0).Value = "O" Then

                                ElemSheet.Cells(nr, nc).Value = ElemSheet.Cells(nr - 1, nc).Value

                            Else : ElemSheet.Cells(nr, nc).Value = frmLogEntry.oXL.ActiveCell.Offset(0, -ev).Value - _
                                frmLogEntry.oXL.ActiveCell.Offset(-1, -ev).Value + ElemSheet.Cells(nr - 1, nc).Value

                            End If

                        ElseIf frmLogEntry.oXL.ActiveCell.Value = "B" Or frmLogEntry.oXL.ActiveCell.Value = "U" Or _
                            frmLogEntry.oXL.ActiveCell.Value = "O" Or frmLogEntry.oXL.ActiveCell.Value = "DG" Then

                            If frmLogEntry.oXL.ActiveCell.Offset(-1, 0).Value = "D" Then

                                ElemSheet.Cells(nr, nc).Value = frmLogEntry.oXL.ActiveCell.Offset(0, -ev).Value - _
                                    frmLogEntry.oXL.ActiveCell.Offset(-1, -ev).Value + ElemSheet.Cells(nr - 1, nc).Value

                            Else : ElemSheet.Cells(nr, nc).Value = ElemSheet.Cells(nr - 1, nc).Value

                            End If

                        Else : Check = False

                            Exit Do

                        End If

                        frmLogEntry.oXL.ActiveCell.Offset(1, 0).Activate()

                    Next nr

                Loop

            Loop Until Check = False

        Catch ex As Exception

            MsgBox("A program error has occurred in " + shortName + " DOWN(): " & ex.Message)

        End Try

    End Sub

    Sub OFFLINE(ByVal ElemSheet, ByVal ev)

        Try

            Dim Check, nr, nc

            Check = True

            Do

                Do Until Check = False

                    For nr = 3 To ElemSheet.Rows.Count

                        nc = 6

                        If frmLogEntry.oXL.ActiveCell.Value = "O" Then

                            If frmLogEntry.oXL.ActiveCell.Offset(-1, 0).Value = "U" Or _
                                frmLogEntry.oXL.ActiveCell.Offset(-1, 0).Value = "DG" Or _
                                frmLogEntry.oXL.ActiveCell.Offset(-1, 0).Value = "B" Or _
                                frmLogEntry.oXL.ActiveCell.Offset(-1, 0).Value = "D" Then

                                ElemSheet.Cells(nr, nc).Value = ElemSheet.Cells(nr - 1, nc).Value

                            Else : ElemSheet.Cells(nr, nc).Value = frmLogEntry.oXL.ActiveCell.Offset(0, -ev).Value - _
                                frmLogEntry.oXL.ActiveCell.Offset(-1, -ev).Value + ElemSheet.Cells(nr - 1, nc).Value

                            End If

                        ElseIf frmLogEntry.oXL.ActiveCell.Value = "B" Or frmLogEntry.oXL.ActiveCell.Value = "U" Or _
                            frmLogEntry.oXL.ActiveCell.Value = "DG" Or frmLogEntry.oXL.ActiveCell.Value = "D" Then

                            If frmLogEntry.oXL.ActiveCell.Offset(-1, 0).Value = "O" Then

                                ElemSheet.Cells(nr, nc).Value = frmLogEntry.oXL.ActiveCell.Offset(0, -ev).Value - _
                                    frmLogEntry.oXL.ActiveCell.Offset(-1, -ev).Value + ElemSheet.Cells(nr - 1, nc).Value

                            Else : ElemSheet.Cells(nr, nc).Value = ElemSheet.Cells(nr - 1, nc).Value

                            End If

                        Else : Check = False

                            Exit Do

                        End If

                        frmLogEntry.oXL.ActiveCell.Offset(1, 0).Activate()

                    Next nr

                Loop

            Loop Until Check = False

        Catch ex As Exception

            MsgBox("A program error has occurred in " + shortName + " OFFLINE(): " & ex.Message)

        End Try

    End Sub

    Sub BREAK(ByVal ElemSheet, ByVal ev)

        Try

            Dim Check, nr, nc

            Check = True

            Do

                Do Until Check = False

                    For nr = 3 To ElemSheet.Rows.Count

                        nc = 7

                        If frmLogEntry.oXL.ActiveCell.Value = "B" Then

                            If frmLogEntry.oXL.ActiveCell.Offset(-1, 0).Value = "D" Or _
                                frmLogEntry.oXL.ActiveCell.Offset(-1, 0).Value = "DG" Or _
                                frmLogEntry.oXL.ActiveCell.Offset(-1, 0).Value = "U" Or _
                                frmLogEntry.oXL.ActiveCell.Offset(-1, 0).Value = "O" Then

                                ElemSheet.Cells(nr, nc).Value = ElemSheet.Cells(nr - 1, nc).Value

                            Else : ElemSheet.Cells(nr, nc).Value = frmLogEntry.oXL.ActiveCell.Offset(0, -ev).Value - _
                                frmLogEntry.oXL.ActiveCell.Offset(-1, -ev).Value + ElemSheet.Cells(nr - 1, nc).Value

                            End If

                        ElseIf frmLogEntry.oXL.ActiveCell.Value = "U" Or frmLogEntry.oXL.ActiveCell.Value = "D" Or _
                            frmLogEntry.oXL.ActiveCell.Value = "O" Or frmLogEntry.oXL.ActiveCell.Value = "DG" Then

                            If frmLogEntry.oXL.ActiveCell.Offset(-1, 0).Value = "B" Then

                                ElemSheet.Cells(nr, nc).Value = frmLogEntry.oXL.ActiveCell.Offset(0, -ev).Value - _
                                    frmLogEntry.oXL.ActiveCell.Offset(-1, -ev).Value + ElemSheet.Cells(nr - 1, nc).Value

                            Else : ElemSheet.Cells(nr, nc).Value = ElemSheet.Cells(nr - 1, nc).Value

                            End If

                        Else : Check = False

                            Exit Do

                        End If

                        frmLogEntry.oXL.ActiveCell.Offset(1, 0).Activate()

                    Next nr

                Loop

            Loop Until Check = False

        Catch ex As Exception

            MsgBox("A program error has occurred in " + shortName + " BREAK(): " & ex.Message)

        End Try

    End Sub

    Private Sub upButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles upButton.Click

        Try

            setStatus(shortName, "U")

            If frmLogEntry.txtLogEntry.Text = "" Then
                frmLogEntry.txtLogEntry.Text = shortName + " is UP"
            Else : frmLogEntry.txtLogEntry.Text = frmLogEntry.txtLogEntry.Text + ";" + shortName + " is UP"
            End If

            'Debug.Print(shortName + " is " + getStatus())

        Catch ex As Exception

            MsgBox("A program error has occurred in " + shortName + "_Click(): " & ex.Message)

        End Try

    End Sub

    Private Sub dgButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles dgButton.Click

        Try

            setStatus(shortName, "DG")

            If frmLogEntry.txtLogEntry.Text = "" Then
                frmLogEntry.txtLogEntry.Text = shortName + " is DGRD"
            Else : frmLogEntry.txtLogEntry.Text = frmLogEntry.txtLogEntry.Text + ";" + shortName + " is DGRD"
            End If

        Catch ex As Exception

            MsgBox("A program error has occurred in " + shortName + "_Click(): " & ex.Message)

        End Try

    End Sub

    Private Sub dnButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles dnButton.Click

        Try

            setStatus(shortName, "D")

            If frmLogEntry.txtLogEntry.Text = "" Then
                frmLogEntry.txtLogEntry.Text = shortName + " is DOWN"
            Else : frmLogEntry.txtLogEntry.Text = frmLogEntry.txtLogEntry.Text + ";" + shortName + " is DOWN"
            End If

        Catch ex As Exception

            MsgBox("A program error has occurred in " + shortName + "_Click(): " & ex.Message)

        End Try

    End Sub

    Private Sub naButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles naButton.Click

        Try

            setStatus(shortName, "O")

            If frmLogEntry.txtLogEntry.Text = "" Then
                frmLogEntry.txtLogEntry.Text = shortName + " is OFFLINE"
            Else : frmLogEntry.txtLogEntry.Text = frmLogEntry.txtLogEntry.Text + ";" + shortName + " is OFFLINE"
            End If

        Catch ex As Exception

            MsgBox("A program error has occurred in " + shortName + "_Click(): " & ex.Message)

        End Try

    End Sub

End Class
