# Chess-Game

The **“Half-Chess”** project is a **full-stack .NET** application that combines a **WinForms client**, an **ASP.NET Core Web API server**, and a **SQL Server database**. It implements a shortened chess variant on an 8×4 board (the right half of a standard chessboard), where each side has a King, Bishop, Knight, Rook, and four pawns. The game follows the standard chess rules with a per-turn timer, and a player who runs out 

of time loses. The **client side (WinForms)** manages the graphical board, game logic, timers, move and check animations, and freehand drawing tools for annotations. The **server side (ASP.NET Core Web API)** handles communication with clients, manages player and game records in the **SQL Server database** through Entity Framework Core, and generates random legal moves as the opponent’s responses. The database serves as a central repository for players and games, allowing tracking and storage of all relevant information.


<img width="100" height="100" alt="Image" src="https://github.com/user-attachments/assets/5b76b72e-c546-40e7-a10f-f13369219dfb" />


## Login screen
<img width="427" height="671" alt="Image" src="https://github.com/user-attachments/assets/5a48a4f1-3840-4915-ae75-ba30e4e4e9c5" />

## Homepage screen
<img width="422" height="672" alt="Image" src="https://github.com/user-attachments/assets/8026de99-4c30-43d6-808e-5a6d36e823dd" />

## Game screen
<img width="457" height="742" alt="Image" src="https://github.com/user-attachments/assets/51b98134-af31-44bc-9b0d-574f58cdf182" />

## Winner screen
<img width="380" height="405" alt="Image" src="https://github.com/user-attachments/assets/502a9521-17cd-4414-a8f7-5add34a25701" />

## Video panel
[Watch the demo](https://drive.google.com/file/d/1mQcfLdVq1upDM6GStftOeHL7DVMN-2jT/view?usp=drive_link)
