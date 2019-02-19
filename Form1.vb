Public Class Form1
    Dim Str As System.IO.StreamReader
    Public ONAY As Boolean
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim YOL As String
        YOL = Application.StartupPath & "\SITELER.txt"
        Str = IO.File.OpenText(YOL)
        Dim SITE() As String = Str.ReadToEnd.Split(vbNewLine)
        ListBox3.Items.AddRange(SITE)
        YOL = Application.StartupPath & "\UZANTILAR.txt"
        Str = IO.File.OpenText(YOL)
        Dim UZANTI() As String = Str.ReadToEnd.Split(vbNewLine)
        ListBox2.Items.AddRange(UZANTI)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim SITE As String
        Dim UZANTI As String
        Dim BAKSITE As String
        Dim SONUC As String
        For I = 0 To ListBox3.Items.Count - 1
            SITE = ListBox3.Items(I)
            If Len(SITE) < 5 Then GoTo 100
            For J = 0 To ListBox2.Items.Count - 1
                UZANTI = ListBox2.Items(J)
                If Len(UZANTI) < 2 Then GoTo 110
                BAKSITE = SITE & UZANTI
                TextBox1.Text = BAKSITE
                WebBrowser1.Navigate(BAKSITE)
                Do Until WebBrowser1.ReadyState = WebBrowserReadyState.Complete
                    Application.DoEvents()
                Loop
                If ONAY = True Then
                    SONUC = BAKSITE & " Bulundu'"
                    ListBox1.Items.Add(SONUC)
                Else
                    SONUC = BAKSITE & " Sonuc Yok'"
                    ListBox1.Items.Add(SONUC)
                End If
110:
            Next
100:
        Next
        MsgBox("Tamamlandı")
    End Sub

    Private Sub WebBrowser1_DocumentCompleted(sender As Object, e As WebBrowserDocumentCompletedEventArgs) Handles WebBrowser1.DocumentCompleted
        Dim K1 As Integer
        K1 = 0
        ONAY = False
        For Each element As HtmlElement In WebBrowser1.Document.All
            K1 = K1 + 1
            If K1 = 6 Then
                If (Mid(element.InnerHtml, 5, 4)) = "?php" Then
                    ONAY = True
                End If
            End If
        Next
    End Sub
End Class
