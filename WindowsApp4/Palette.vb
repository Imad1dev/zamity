Imports System.Data.OleDb


Public Class Palette


    ' Method to get the database connection
    Private Function GetConnection() As OleDbConnection
        ' Replace with your actual database file path
        Dim connectionString As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\asus\Documents\WindowsApp4\WindowsApp4\Projet2.accdb"
        Return New OleDbConnection(connectionString)
    End Function


    Private Sub NouveauBR_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PopulateComboBox(ComboBox1, "SELECT NumeroBR FROM Reception", "NumeroBR")
    End Sub
    ' Method to populate a ComboBox
    Private Sub PopulateComboBox(comboBox As ComboBox, selectQuery As String, columnName As String)
        Using connection As OleDbConnection = GetConnection()
            Using command As New OleDbCommand(selectQuery, connection)
                Try
                    connection.Open()
                    Using reader As OleDbDataReader = command.ExecuteReader()
                        While reader.Read()
                            comboBox.Items.Add(reader(columnName).ToString())
                        End While
                    End Using
                Catch ex As Exception
                    MessageBox.Show("Error: " & ex.Message)
                End Try
            End Using
        End Using
    End Sub


    ' Method to insert data into the database
    Private Sub InsertData(insertQuery As String, parameters As OleDbParameter())
        Using connection As OleDbConnection = GetConnection()
            Using command As New OleDbCommand(insertQuery, connection)
                command.Parameters.AddRange(parameters)

                Try
                    connection.Open()
                    Dim result As Integer = command.ExecuteNonQuery()

                    ' Check if the insert was successful
                    If result > 0 Then
                        MessageBox.Show("Data inserted successfully.")
                    Else
                        MessageBox.Show("Data insert failed.")
                    End If
                Catch ex As Exception
                    MessageBox.Show("Error: " & ex.Message)
                End Try
            End Using
        End Using
    End Sub

    ' Event handler for the Insert button
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Check if required fields are filled and Poids, Cout, NombresdeCaisses are numeric
        If String.IsNullOrEmpty(TextBox2.Text) Then
            Label15.Text = "Please fill NumeroBR"
        Else
            ' All required fields are filled and numeric, insert data
            Dim insertQuery As String = "INSERT INTO Ferme (Ferme) " & "VALUES (?)"

            ' Create parameters
            Dim parameters As OleDbParameter() = {
                New OleDbParameter("@NumeroBR", TextBox2.Text)
            }

            ' Call the insert method
            InsertData(insertQuery, parameters)
        End If
    End Sub

    Private Sub Palette_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class