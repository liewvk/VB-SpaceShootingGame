<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        components = New ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        picPlayer = New PictureBox()
        picEnemy = New PictureBox()
        picLaser = New PictureBox()
        lblTitle = New Label()
        lblScore = New Label()
        lblHealth = New Label()
        btnStart = New Button()
        btnRestart = New Button()
        btnExit = New Button()
        gameTimer = New Timer(components)
        lblGameOver = New Label()
        CType(picPlayer, ComponentModel.ISupportInitialize).BeginInit()
        CType(picEnemy, ComponentModel.ISupportInitialize).BeginInit()
        CType(picLaser, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' picPlayer
        ' 
        picPlayer.Image = CType(resources.GetObject("picPlayer.Image"), Image)
        picPlayer.Location = New Point(264, 337)
        picPlayer.Name = "picPlayer"
        picPlayer.Size = New Size(112, 70)
        picPlayer.SizeMode = PictureBoxSizeMode.StretchImage
        picPlayer.TabIndex = 0
        picPlayer.TabStop = False
        ' 
        ' picEnemy
        ' 
        picEnemy.Image = CType(resources.GetObject("picEnemy.Image"), Image)
        picEnemy.Location = New Point(264, 81)
        picEnemy.Name = "picEnemy"
        picEnemy.Size = New Size(105, 69)
        picEnemy.SizeMode = PictureBoxSizeMode.StretchImage
        picEnemy.TabIndex = 0
        picEnemy.TabStop = False
        ' 
        ' picLaser
        ' 
        picLaser.Image = CType(resources.GetObject("picLaser.Image"), Image)
        picLaser.Location = New Point(457, 164)
        picLaser.Name = "picLaser"
        picLaser.Size = New Size(76, 81)
        picLaser.SizeMode = PictureBoxSizeMode.StretchImage
        picLaser.TabIndex = 0
        picLaser.TabStop = False
        ' 
        ' lblTitle
        ' 
        lblTitle.AutoSize = True
        lblTitle.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblTitle.Location = New Point(264, 21)
        lblTitle.Name = "lblTitle"
        lblTitle.Size = New Size(195, 28)
        lblTitle.TabIndex = 1
        lblTitle.Text = "Space Shooter Game"
        ' 
        ' lblScore
        ' 
        lblScore.AutoSize = True
        lblScore.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblScore.Location = New Point(15, 38)
        lblScore.Name = "lblScore"
        lblScore.Size = New Size(56, 25)
        lblScore.TabIndex = 2
        lblScore.Text = "Score"
        lblScore.UseWaitCursor = True
        ' 
        ' lblHealth
        ' 
        lblHealth.AutoSize = True
        lblHealth.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblHealth.Location = New Point(15, 103)
        lblHealth.Name = "lblHealth"
        lblHealth.Size = New Size(63, 25)
        lblHealth.TabIndex = 2
        lblHealth.Text = "Health"
        lblHealth.UseWaitCursor = True
        ' 
        ' btnStart
        ' 
        btnStart.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        btnStart.Location = New Point(24, 183)
        btnStart.Name = "btnStart"
        btnStart.Size = New Size(123, 32)
        btnStart.TabIndex = 3
        btnStart.Text = "Start"
        btnStart.UseVisualStyleBackColor = True
        ' 
        ' btnRestart
        ' 
        btnRestart.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        btnRestart.Location = New Point(24, 241)
        btnRestart.Name = "btnRestart"
        btnRestart.Size = New Size(123, 32)
        btnRestart.TabIndex = 3
        btnRestart.Text = "Restart"
        btnRestart.UseVisualStyleBackColor = True
        ' 
        ' btnExit
        ' 
        btnExit.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        btnExit.Location = New Point(24, 303)
        btnExit.Name = "btnExit"
        btnExit.Size = New Size(123, 32)
        btnExit.TabIndex = 3
        btnExit.Text = "Exit"
        btnExit.UseVisualStyleBackColor = True
        ' 
        ' gameTimer
        ' 
        ' 
        ' lblGameOver
        ' 
        lblGameOver.AutoSize = True
        lblGameOver.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblGameOver.Location = New Point(37, 369)
        lblGameOver.Name = "lblGameOver"
        lblGameOver.Size = New Size(101, 25)
        lblGameOver.TabIndex = 4
        lblGameOver.Text = "Game Over"
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(lblGameOver)
        Controls.Add(btnExit)
        Controls.Add(btnRestart)
        Controls.Add(btnStart)
        Controls.Add(lblHealth)
        Controls.Add(lblScore)
        Controls.Add(lblTitle)
        Controls.Add(picLaser)
        Controls.Add(picEnemy)
        Controls.Add(picPlayer)
        Name = "Form1"
        Text = "Form1"
        CType(picPlayer, ComponentModel.ISupportInitialize).EndInit()
        CType(picEnemy, ComponentModel.ISupportInitialize).EndInit()
        CType(picLaser, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents picPlayer As PictureBox
    Friend WithEvents picEnemy As PictureBox
    Friend WithEvents picLaser As PictureBox
    Friend WithEvents lblTitle As Label
    Friend WithEvents lblScore As Label
    Friend WithEvents lblHealth As Label
    Friend WithEvents btnStart As Button
    Friend WithEvents btnRestart As Button
    Friend WithEvents btnExit As Button
    Friend WithEvents gameTimer As Timer
    Friend WithEvents lblGameOver As Label

End Class
