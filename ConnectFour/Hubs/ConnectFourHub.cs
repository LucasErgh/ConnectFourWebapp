using Microsoft.AspNetCore.SignalR;

namespace ConnectFour.Hubs {
    public class ConnectFourHub : Hub {
        public async Task SendMessage(string GameRoom, string Message){
            await Clients.All.SendAsync("ReceiveMessage", GameRoom, Message);
        }
    }
}
public enum Token{
        empty,
        red,
        blue
    }
public class ConnectFourGame{
    public Token playerTurn {get; set;} = Token.blue;
    public bool gameOver {get; set;} = false;
    Token[,] board;       
    Token curPlayer = Token.blue;
    public ConnectFourGame(){
        board = new Token[6,7];
    }
    public void resetBoard(){
        for(int j = 0; j < 6; ++j){
            for(int k = 0; k < 7; ++k){
                board[j,k] = Token.empty;
            }
        }
        gameOver = false;
    }
    // this function returns true if the move is valid, if invalid return false
    public bool playerMove(int col){
        // find first empty row in column
        for(int i = 5; i >= 0; --i){
            if(board[i, col] == Token.empty) {
                board[i, col] = curPlayer;
                if(curPlayer == Token.red) curPlayer = Token.blue;
                else curPlayer = Token.red;
                return true;
            }
        }
        if(curPlayer == Token.red) curPlayer = Token.blue;
        else curPlayer = Token.red;           
        return false; 
    }
    public Token checkGameOver(){
        for(int r = 0; r < 6; ++r){
            for(int c = 0; c < 7; ++c){
                // check for vertical win
                if ((c + 3 < 7 && 
                    board[r,c] == board[r,c + 1] &&  
                    board[r,c] == board[r,c + 2] && 
                    board[r,c] == board[r,c + 3] && 
                    board[r,c] != Token.empty) || 
                // check for horizontal win 
                    (r + 3 < 6 && 
                    board[r,c] == board[r + 1,c] &&  
                    board[r,c] == board[r + 2,c] && 
                    board[r,c] == board[r + 3,c] && 
                    board[r,c] != Token.empty) ||
                // Check diagonal (top-left to bottom-right)
                    (r + 3 < 6 && c + 3 < 7 &&
                    board[r, c] == board[r + 1, c + 1] &&
                    board[r, c] == board[r + 2, c + 2] &&
                    board[r, c] == board[r + 3, c + 3] && 
                    board[r,c] != Token.empty) ||
                // Check diagonal (top-right to bottom-left)
                    (r + 3 < 6 && c - 3 >= 0 &&
                    board[r, c] == board[r + 1, c - 1] &&
                    board[r, c] == board[r + 2, c - 2] &&
                    board[r, c] == board[r + 3, c - 3] && 
                    board[r,c] != Token.empty))
                {
                    gameOver = true;
                    return board[r,c];
                }
            }
        }
        gameOver = false;
        return Token.empty;
    }
    public Token getCell(int row, int col){
        return board[row, col];
    }
}
