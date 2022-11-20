Imports System.IO.Ports
Public Class ClassJournal
    Dim port As SerialPort = New SerialPort()
    Private Function Button1_Click(sender As Object, e As EventArgs) As Boolean Handles Button1.Click

        If Not port.IsOpen Then Return False

        Try
            System.Threading.Thread.Sleep(500)
            port.WriteLine("AT\r\n")
            System.Threading.Thread.Sleep(500)

            port.Write("AT+CMGF=1\r\n")
            System.Threading.Thread.Sleep(500)
        Catch ex As Exception

        End Try


            catch { return false; }

            Try
            {
                port.Write("AT+CMGS=\"" + telnumber + "\"" + "\r\n"); // задаем номер мобильного телефона получаталя смс
                System.Threading.Thread.Sleep(500);

                port.Write(textsms + char.ConvertFromUtf32(26) + "\r\n"); // отправляем текст смс
                System.Threading.Thread.Sleep(500);
            }
            catch { return false; }

                Try
            {
                string recievedData;
                recievedData = port.ReadExisting();

                    If (recievedData.Contains("ERROR")) Then
                {
                    return false;
                }

            }
            catch { }

            return true;
    End Function
End Class
