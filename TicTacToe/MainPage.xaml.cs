namespace TicTacToe
{
    public partial class MainPage : ContentPage
    {
        private string currentPlayer;
        private bool gameEnded;
        private string[,] board; // Tabuleiro lógico do jogo

        public MainPage()
        {
            InitializeComponent();
            StartNewGame();
        }

        private void StartNewGame()
        {
            currentPlayer = "X";
            gameEnded = false;
            board = new string[3, 3]; // Tabuleiro 3x3
            StatusLabel.Text = $"Vez do Jogador: {currentPlayer}";

            // Assume que o XAML tem uma <Grid x:Name="GameBoardGrid">
            // Este loop limpa a interface
            foreach (var view in GameBoardGrid.Children)
            {
                if (view is Button button)
                {
                    button.Text = string.Empty;
                    button.IsEnabled = true;
                }
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (gameEnded) return;

            var button = sender as Button;
            if (button == null || !string.IsNullOrEmpty(button.Text)) return;

            // Lógica para obter a posição correta
            // Assumindo que a GameBoardGrid no XAML tem linhas 0, 1, 2
            int row = Grid.GetRow(button);
            int col = Grid.GetColumn(button);

            // Atualiza o tabuleiro lógico e a interface
            board[row, col] = currentPlayer;
            button.Text = currentPlayer;
            button.IsEnabled = false;

            // Verifica o estado do jogo
            if (CheckForWinner())
            {
                gameEnded = true;
                StatusLabel.Text = $"Jogador {currentPlayer} Venceu!";
            }
            else if (IsBoardFull())
            {
                gameEnded = true;
                StatusLabel.Text = "Deu velha! Empate!";
            }
            else
            {
                // Alterna o jogador
                currentPlayer = (currentPlayer == "X") ? "O" : "X";
                StatusLabel.Text = $"Vez do Jogador: {currentPlayer}";
            }
        }

        private bool IsBoardFull()
        {
            // Percorre o tabuleiro 3x3
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    // Se encontrar qualquer espaço vazio, o tabuleiro não está cheio
                    if (string.IsNullOrEmpty(board[i, j]))
                    {
                        return false;
                    }
                }
            }
            // Se o loop terminar, o tabuleiro está cheio
            return true;
        }

        private bool CheckForWinner()
        {
            // Verificar Linhas Horizontais (3x3)
            for (int i = 0; i < 3; i++)
            {
                if (!string.IsNullOrEmpty(board[i, 0]) &&
                    board[i, 0] == board[i, 1] &&
                    board[i, 1] == board[i, 2])
                {
                    return true;
                }
            }

            // Verificar Colunas Verticais (3x3)
            for (int j = 0; j < 3; j++)
            {
                if (!string.IsNullOrEmpty(board[0, j]) &&
                    board[0, j] == board[1, j] &&
                    board[1, j] == board[2, j])
                {
                    return true;
                }
            }

            // Verificar Diagonal Principal (3x3)
            if (!string.IsNullOrEmpty(board[0, 0]) &&
                board[0, 0] == board[1, 1] &&
                board[1, 1] == board[2, 2])
            {
                return true;
            }

            // Verificar Diagonal Secundária (3x3)
            if (!string.IsNullOrEmpty(board[0, 2]) &&
                board[0, 2] == board[1, 1] &&
                board[1, 1] == board[2, 0])
            {
                return true;
            }

            return false;
        }

        // Garanta que o nome do evento no XAML seja exatamente "ResetButton_Clicked"
        private void ResetButton_Clicked(object sender, EventArgs e)
        {
            StartNewGame();
        }
    }
}