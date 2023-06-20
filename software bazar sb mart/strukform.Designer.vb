<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class strukform
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
        Me.penjual_bazar = New CrystalDecisions.Windows.Forms.CrystalReportViewer()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.dtppindah2 = New System.Windows.Forms.DateTimePicker()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.dtppindah1 = New System.Windows.Forms.DateTimePicker()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'penjual_bazar
        '
        Me.penjual_bazar.ActiveViewIndex = -1
        Me.penjual_bazar.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.penjual_bazar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.penjual_bazar.Cursor = System.Windows.Forms.Cursors.Default
        Me.penjual_bazar.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.penjual_bazar.Location = New System.Drawing.Point(0, 96)
        Me.penjual_bazar.Name = "penjual_bazar"
        Me.penjual_bazar.Size = New System.Drawing.Size(963, 630)
        Me.penjual_bazar.TabIndex = 0
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(134, 10)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(64, 13)
        Me.Label9.TabIndex = 28
        Me.Label9.Text = "Priode Akhir"
        '
        'dtppindah2
        '
        Me.dtppindah2.CustomFormat = "yyyy-MM-dd"
        Me.dtppindah2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtppindah2.Location = New System.Drawing.Point(137, 26)
        Me.dtppindah2.Name = "dtppindah2"
        Me.dtppindah2.Size = New System.Drawing.Size(98, 20)
        Me.dtppindah2.TabIndex = 27
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(34, 10)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(63, 13)
        Me.Label10.TabIndex = 26
        Me.Label10.Text = "Priode Awal"
        '
        'dtppindah1
        '
        Me.dtppindah1.CustomFormat = "yyyy-MM-dd"
        Me.dtppindah1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtppindah1.Location = New System.Drawing.Point(37, 26)
        Me.dtppindah1.Name = "dtppindah1"
        Me.dtppindah1.Size = New System.Drawing.Size(94, 20)
        Me.dtppindah1.TabIndex = 25
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.DodgerBlue
        Me.Button2.FlatAppearance.BorderSize = 0
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.ForeColor = System.Drawing.Color.White
        Me.Button2.Location = New System.Drawing.Point(241, 20)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 31)
        Me.Button2.TabIndex = 24
        Me.Button2.Text = "Cetak"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Button2)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.dtppindah1)
        Me.Panel1.Controls.Add(Me.dtppindah2)
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(963, 54)
        Me.Panel1.TabIndex = 29
        '
        'strukform
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(963, 726)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.penjual_bazar)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "strukform"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Hasil penjualan Bazar"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents penjual_bazar As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents dtppindah2 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents dtppindah1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
End Class
