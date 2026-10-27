
Option Strict On

    Imports System.Collections.Generic
    Imports System.Drawing
    Imports System.Windows.Forms

    Public Class Form1

        Private ReadOnly random As New Random()

        Private ReadOnly enemies As New List(Of PictureBox)()
        Private ReadOnly lasers As New List(Of PictureBox)()
        Private ReadOnly enemyLasers As New List(Of PictureBox)()

        Private playArea As Control

        Private Const PlayerSpeed As Integer = 8
        Private Const LaserSpeed As Integer = 14
        Private Const EnemyLaserSpeed As Integer = 7
        Private Const ShotDelay As Integer = 200

        Private score As Integer
        Private health As Integer = 3
        Private enemySpeed As Integer = 3

        Private spawnElapsed As Integer
        Private enemyShotElapsed As Integer
        Private shotCooldown As Integer

        Private movingLeft As Boolean
        Private movingRight As Boolean
        Private shooting As Boolean
        Private running As Boolean

        Private Sub Form1_Load(
            sender As Object,
            e As EventArgs
        ) Handles MyBase.Load

            playArea = picPlayer.Parent

            Me.KeyPreview = True
            Me.DoubleBuffered = True

            gameTimer.Stop()
            gameTimer.Interval = 20

            picPlayer.SizeMode = PictureBoxSizeMode.Zoom
            picPlayer.TabStop = False

            ' These controls provide images for new sprites.
            picEnemy.Visible = False
            picLaser.Visible = False

            lblScore.Text = "Score: 0"
            lblHealth.Text = "Health: 3"
            lblGameOver.Visible = False
            btnRestart.Visible = False

            PositionPlayer()
        End Sub

        Private Sub PositionPlayer()
            picPlayer.Left =
                Math.Max(0,
                         (playArea.ClientSize.Width - picPlayer.Width) \ 2)

            ' Leave room for buttons placed along the bottom.
            picPlayer.Top =
                Math.Max(0,
                         playArea.ClientSize.Height - picPlayer.Height - 60)

            picPlayer.BringToFront()
        End Sub

        Private Sub StartGame()
            gameTimer.Stop()
            running = False

            ClearSprites(enemies)
            ClearSprites(lasers)
            ClearSprites(enemyLasers)
            ResetKeys()

            score = 0
            health = 3
            enemySpeed = 3

            spawnElapsed = 0
            enemyShotElapsed = 0
            shotCooldown = 0

            lblScore.Text = "Score: 0"
            lblHealth.Text = "Health: 3"
            lblGameOver.Visible = False

            btnStart.Visible = False
            btnRestart.Visible = False

            PositionPlayer()
            SpawnEnemy()

            running = True
            Me.ActiveControl = Nothing
            Me.Focus()

            gameTimer.Start()
        End Sub

        ' Every animation update happens here.
        Private Sub gameTimer_Tick(
            sender As Object,
            e As EventArgs
        ) Handles gameTimer.Tick

            If Not running Then Return

            MovePlayer()

            shotCooldown =
                Math.Max(0, shotCooldown - gameTimer.Interval)

            If shooting AndAlso shotCooldown = 0 Then
                ShootLaser()
                shotCooldown = ShotDelay
            End If

            MoveLasers()
            MoveEnemies()

            If Not running Then Return

            MoveEnemyLasers()

            If Not running Then Return

            spawnElapsed += gameTimer.Interval
            enemyShotElapsed += gameTimer.Interval

            Dim spawnDelay As Integer =
                Math.Max(350, 1000 - (score \ 50) * 100)

            If spawnElapsed >= spawnDelay Then
                spawnElapsed = 0
                SpawnEnemy()
            End If

            If enemyShotElapsed >= 1200 Then
                enemyShotElapsed = 0
                ShootEnemyLaser()
            End If

            BringInterfaceToFront()
        End Sub

        Private Sub MovePlayer()
            Dim direction As Integer = 0

            If movingLeft Then direction -= 1
            If movingRight Then direction += 1

            Dim maxLeft As Integer =
                Math.Max(0, playArea.ClientSize.Width - picPlayer.Width)

            picPlayer.Left =
                Math.Max(0,
                         Math.Min(maxLeft,
                                  picPlayer.Left + direction * PlayerSpeed))
        End Sub

        Private Function CreateSprite(
            template As PictureBox,
            spriteSize As Size,
            fallbackColor As Color
        ) As PictureBox

            Dim sprite As New PictureBox With {
                .Size = spriteSize,
                .Image = template.Image,
                .SizeMode = PictureBoxSizeMode.Zoom,
                .TabStop = False
            }

            If sprite.Image Is Nothing Then
                sprite.BackColor = fallbackColor
            Else
                sprite.BackColor = Color.Transparent
            End If

            playArea.Controls.Add(sprite)
            sprite.BringToFront()

            Return sprite
        End Function

        Private Sub SpawnEnemy()
            Dim enemy As PictureBox =
                CreateSprite(picEnemy, New Size(48, 48), Color.Red)

            Dim maxLeft As Integer =
                Math.Max(0, playArea.ClientSize.Width - enemy.Width)

            enemy.Location =
                New Point(random.Next(maxLeft + 1), -enemy.Height)

            enemies.Add(enemy)
        End Sub

        Private Sub ShootLaser()
            ' A narrow PictureBox stretches the supplied laser image
            ' so its transparent side margins do not make it too thin.
            Dim laser As PictureBox =
                CreateSprite(picLaser, New Size(10, 30), Color.Cyan)

            laser.SizeMode = PictureBoxSizeMode.StretchImage

            laser.Location =
                New Point(
                    picPlayer.Left + (picPlayer.Width - laser.Width) \ 2,
                    picPlayer.Top - laser.Height
                )

            lasers.Add(laser)
        End Sub

        Private Sub ShootEnemyLaser()
            ' Only choose enemies that are visible above the player.
            Dim candidates As New List(Of PictureBox)()

            For Each enemy As PictureBox In enemies
                If enemy.Top >= 0 AndAlso
                   enemy.Bottom < picPlayer.Top Then

                    candidates.Add(enemy)
                End If
            Next

            If candidates.Count = 0 Then Return

            Dim shooter As PictureBox =
                candidates(random.Next(candidates.Count))

            ' Enemy projectiles use an orange rectangle.
            Dim laser As New PictureBox With {
                .Size = New Size(6, 20),
                .BackColor = Color.OrangeRed,
                .TabStop = False
            }

            laser.Location =
                New Point(
                    shooter.Left + (shooter.Width - laser.Width) \ 2,
                    shooter.Bottom
                )

            playArea.Controls.Add(laser)
            laser.BringToFront()
            enemyLasers.Add(laser)
        End Sub

        Private Sub MoveLasers()
            ' Reverse loops allow sprites to be removed safely.
            For i As Integer = lasers.Count - 1 To 0 Step -1
                Dim laser As PictureBox = lasers(i)

                laser.Top -= LaserSpeed

                If laser.Bottom <= 0 Then
                    RemoveSprite(lasers, i)
                    Continue For
                End If

                For j As Integer = enemies.Count - 1 To 0 Step -1
                    If laser.Bounds.IntersectsWith(enemies(j).Bounds) Then
                        RemoveSprite(enemies, j)
                        RemoveSprite(lasers, i)

                        score += 10
                        lblScore.Text = "Score: " & score.ToString()

                        IncreaseDifficulty()
                        Exit For
                    End If
                Next
            Next
        End Sub

        Private Sub MoveEnemies()
            For i As Integer = enemies.Count - 1 To 0 Step -1
                Dim enemy As PictureBox = enemies(i)

                enemy.Top += enemySpeed

                If enemy.Bounds.IntersectsWith(picPlayer.Bounds) Then
                    ' Remove it first so one enemy causes one hit.
                    RemoveSprite(enemies, i)
                    HandlePlayerHit()

                    If Not running Then Return

                ElseIf enemy.Top >= playArea.ClientSize.Height Then
                    RemoveSprite(enemies, i)
                End If
            Next
        End Sub

        Private Sub MoveEnemyLasers()
            For i As Integer = enemyLasers.Count - 1 To 0 Step -1
                Dim laser As PictureBox = enemyLasers(i)

                laser.Top += EnemyLaserSpeed

                If laser.Bounds.IntersectsWith(picPlayer.Bounds) Then
                    RemoveSprite(enemyLasers, i)
                    HandlePlayerHit()

                    If Not running Then Return

                ElseIf laser.Top >= playArea.ClientSize.Height Then
                    RemoveSprite(enemyLasers, i)
                End If
            Next
        End Sub

        Private Sub HandlePlayerHit()
            health = Math.Max(0, health - 1)
            lblHealth.Text = "Health: " & health.ToString()

            If health = 0 Then EndGame()
        End Sub

        Private Sub IncreaseDifficulty()
            If score >= 100 Then
                enemySpeed = 6
            ElseIf score >= 50 Then
                enemySpeed = 4
            Else
                enemySpeed = 3
            End If
        End Sub

        Private Sub RemoveSprite(
            sprites As List(Of PictureBox),
            index As Integer
        )
            Dim sprite As PictureBox = sprites(index)

            sprites.RemoveAt(index)
            playArea.Controls.Remove(sprite)

            ' Images belong to the template controls.
            sprite.Image = Nothing
            sprite.Dispose()
        End Sub

        Private Sub ClearSprites(sprites As List(Of PictureBox))
            For i As Integer = sprites.Count - 1 To 0 Step -1
                RemoveSprite(sprites, i)
            Next
        End Sub

        Private Sub EndGame()
            running = False
            gameTimer.Stop()
            ResetKeys()

            lblGameOver.Text =
                "Game Over! Final Score: " & score.ToString()

            lblGameOver.Visible = True
            btnRestart.Visible = True
            btnStart.Visible = True

            BringInterfaceToFront()
        End Sub

        Private Sub BringInterfaceToFront()
            lblScore.BringToFront()
            lblHealth.BringToFront()
            lblGameOver.BringToFront()

            btnStart.BringToFront()
            btnRestart.BringToFront()
            btnExit.BringToFront()
        End Sub

        ' Capture key presses before focused buttons process them.
        ' Holding a key gives smooth movement and repeated shooting.
        Protected Overrides Function ProcessKeyPreview(
            ByRef m As Message
        ) As Boolean

            Const WM_KEYDOWN As Integer = &H100
            Const WM_KEYUP As Integer = &H101

            If running AndAlso
               (m.Msg = WM_KEYDOWN OrElse m.Msg = WM_KEYUP) Then

                Dim pressed As Boolean = (m.Msg = WM_KEYDOWN)
                Dim key As Keys = CType(m.WParam.ToInt32(), Keys)

                Select Case key
                    Case Keys.Left, Keys.A
                        movingLeft = pressed
                        Return True

                    Case Keys.Right, Keys.D
                        movingRight = pressed
                        Return True

                    Case Keys.Space
                        shooting = pressed
                        Return True
                End Select
            End If

            Return MyBase.ProcessKeyPreview(m)
        End Function

        Private Sub ResetKeys()
            movingLeft = False
            movingRight = False
            shooting = False
        End Sub

        Private Sub Form1_Deactivate(
            sender As Object,
            e As EventArgs
        ) Handles MyBase.Deactivate

            ResetKeys()

            ' Pause when switching to another window.
            gameTimer.Stop()
        End Sub

        Private Sub Form1_Activated(
            sender As Object,
            e As EventArgs
        ) Handles MyBase.Activated

            If running Then gameTimer.Start()
        End Sub

        Private Sub btnStart_Click(
            sender As Object,
            e As EventArgs
        ) Handles btnStart.Click

            StartGame()
        End Sub

        Private Sub btnRestart_Click(
            sender As Object,
            e As EventArgs
        ) Handles btnRestart.Click

            StartGame()
        End Sub

        Private Sub btnExit_Click(
            sender As Object,
            e As EventArgs
        ) Handles btnExit.Click

            Me.Close()
        End Sub

        Private Sub Form1_FormClosed(
            sender As Object,
            e As FormClosedEventArgs
        ) Handles MyBase.FormClosed

            running = False
            gameTimer.Stop()

            ClearSprites(enemies)
            ClearSprites(lasers)
            ClearSprites(enemyLasers)
        End Sub

    End Class
