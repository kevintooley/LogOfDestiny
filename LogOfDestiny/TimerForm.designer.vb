<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TimerForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.startButton = New System.Windows.Forms.Button()
        Me.hourTensUpDown = New System.Windows.Forms.DomainUpDown()
        Me.hourOnesUpDown = New System.Windows.Forms.DomainUpDown()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.minTensUpDown = New System.Windows.Forms.DomainUpDown()
        Me.minOnesUpDown = New System.Windows.Forms.DomainUpDown()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'startButton
        '
        Me.startButton.Location = New System.Drawing.Point(113, 31)
        Me.startButton.Name = "startButton"
        Me.startButton.Size = New System.Drawing.Size(75, 30)
        Me.startButton.TabIndex = 0
        Me.startButton.Text = "Start"
        Me.startButton.UseVisualStyleBackColor = True
        '
        'hourTensUpDown
        '
        Me.hourTensUpDown.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.hourTensUpDown.Items.Add("9")
        Me.hourTensUpDown.Items.Add("8")
        Me.hourTensUpDown.Items.Add("7")
        Me.hourTensUpDown.Items.Add("6")
        Me.hourTensUpDown.Items.Add("5")
        Me.hourTensUpDown.Items.Add("4")
        Me.hourTensUpDown.Items.Add("3")
        Me.hourTensUpDown.Items.Add("2")
        Me.hourTensUpDown.Items.Add("1")
        Me.hourTensUpDown.Items.Add("0")
        Me.hourTensUpDown.Location = New System.Drawing.Point(76, 3)
        Me.hourTensUpDown.Name = "hourTensUpDown"
        Me.hourTensUpDown.Size = New System.Drawing.Size(30, 26)
        Me.hourTensUpDown.TabIndex = 1
        Me.hourTensUpDown.Text = "0"
        '
        'hourOnesUpDown
        '
        Me.hourOnesUpDown.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.hourOnesUpDown.Items.Add("9")
        Me.hourOnesUpDown.Items.Add("8")
        Me.hourOnesUpDown.Items.Add("7")
        Me.hourOnesUpDown.Items.Add("6")
        Me.hourOnesUpDown.Items.Add("5")
        Me.hourOnesUpDown.Items.Add("4")
        Me.hourOnesUpDown.Items.Add("3")
        Me.hourOnesUpDown.Items.Add("2")
        Me.hourOnesUpDown.Items.Add("1")
        Me.hourOnesUpDown.Items.Add("0")
        Me.hourOnesUpDown.Location = New System.Drawing.Point(110, 3)
        Me.hourOnesUpDown.Name = "hourOnesUpDown"
        Me.hourOnesUpDown.Size = New System.Drawing.Size(30, 26)
        Me.hourOnesUpDown.TabIndex = 2
        Me.hourOnesUpDown.Text = "0"
        '
        'Timer1
        '
        Me.Timer1.Interval = 1000
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.WindowText
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label1.Font = New System.Drawing.Font("Courier New", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Red
        Me.Label1.Location = New System.Drawing.Point(11, 10)
        Me.Label1.MinimumSize = New System.Drawing.Size(90, 33)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(90, 56)
        Me.Label1.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.WindowText
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label2.Font = New System.Drawing.Font("Courier New", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Red
        Me.Label2.Location = New System.Drawing.Point(117, 10)
        Me.Label2.MinimumSize = New System.Drawing.Size(90, 33)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(90, 56)
        Me.Label2.TabIndex = 4
        '
        'minTensUpDown
        '
        Me.minTensUpDown.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.minTensUpDown.Items.Add("5")
        Me.minTensUpDown.Items.Add("4")
        Me.minTensUpDown.Items.Add("3")
        Me.minTensUpDown.Items.Add("2")
        Me.minTensUpDown.Items.Add("1")
        Me.minTensUpDown.Items.Add("0")
        Me.minTensUpDown.Location = New System.Drawing.Point(161, 3)
        Me.minTensUpDown.Name = "minTensUpDown"
        Me.minTensUpDown.Size = New System.Drawing.Size(30, 26)
        Me.minTensUpDown.TabIndex = 7
        Me.minTensUpDown.Text = "0"
        '
        'minOnesUpDown
        '
        Me.minOnesUpDown.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.minOnesUpDown.Items.Add("9")
        Me.minOnesUpDown.Items.Add("8")
        Me.minOnesUpDown.Items.Add("7")
        Me.minOnesUpDown.Items.Add("6")
        Me.minOnesUpDown.Items.Add("5")
        Me.minOnesUpDown.Items.Add("4")
        Me.minOnesUpDown.Items.Add("3")
        Me.minOnesUpDown.Items.Add("2")
        Me.minOnesUpDown.Items.Add("1")
        Me.minOnesUpDown.Items.Add("0")
        Me.minOnesUpDown.Location = New System.Drawing.Point(194, 3)
        Me.minOnesUpDown.Name = "minOnesUpDown"
        Me.minOnesUpDown.Size = New System.Drawing.Size(30, 26)
        Me.minOnesUpDown.TabIndex = 8
        Me.minOnesUpDown.Text = "0"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label5.Location = New System.Drawing.Point(92, 2)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(14, 20)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = ":"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(93, 8)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(38, 55)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = ":"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(198, 8)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(38, 55)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = ":"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.WindowText
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label4.Font = New System.Drawing.Font("Courier New", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Red
        Me.Label4.Location = New System.Drawing.Point(222, 10)
        Me.Label4.MinimumSize = New System.Drawing.Size(90, 33)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(90, 56)
        Me.Label4.TabIndex = 12
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.Control
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.startButton)
        Me.Panel1.Controls.Add(Me.minOnesUpDown)
        Me.Panel1.Controls.Add(Me.minTensUpDown)
        Me.Panel1.Controls.Add(Me.hourOnesUpDown)
        Me.Panel1.Controls.Add(Me.hourTensUpDown)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Location = New System.Drawing.Point(12, 72)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(300, 66)
        Me.Panel1.TabIndex = 13
        '
        'TimerForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(321, 143)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label3)
        Me.Name = "TimerForm"
        Me.Text = "Time Remaining"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents startButton As System.Windows.Forms.Button
    Friend WithEvents hourTensUpDown As System.Windows.Forms.DomainUpDown
    Friend WithEvents hourOnesUpDown As System.Windows.Forms.DomainUpDown
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents minTensUpDown As System.Windows.Forms.DomainUpDown
    Friend WithEvents minOnesUpDown As System.Windows.Forms.DomainUpDown
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel

End Class
