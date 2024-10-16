using MathLibrary;
using Raylib_cs;
using System.ComponentModel;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;

namespace Sandbox
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();

            void CreateQuadrant1CoordinatePlane()
            {
                //Draw horizontal lines
                int yValue = 0;
                while (yValue <= 480)
                {
                    if (yValue == 480)
                    {
                        Raylib.DrawRectangle(460, 0, 2, 460, Color.Black);
                        break;
                    }
                    Raylib.DrawRectangle(20, yValue, 1000, 2, Color.Black);
                    yValue += 20;
                }

                //Draw vertical lines
                int xValue = 20;
                while (xValue <= 800)
                {

                    Raylib.DrawRectangle(xValue, 0, 2, 460, Color.Black);
                    xValue += 20;

                }

                //Lable Horizontal lines / y Values
                int yLable = 0;
                int yLablePosition = 460;
                while (yLable <= 24)
                {

                    Raylib.DrawText("" + yLable + "", 5, yLablePosition, 10, Color.Black);
                    yLable++;
                    yLablePosition -= 20;
                }

                //Lable Vertical lines / x values
                int xLable = 0;
                int xLablePosition = 20;
                while (xLable <= 39)
                {

                    Raylib.DrawText("" + xLable + "", xLablePosition, 470, 10, Color.Black);
                    xLable++;
                    xLablePosition += 20;
                }
            }

            /// <summary>
            /// Luigi's Mansion: Dark Moon 3
            /// </summary>
            Raylib.InitWindow(1600, 960, "Hello World");
            MathLibrary.Vector2 screenDimensions =
                new MathLibrary.Vector2(Raylib.GetScreenWidth(), Raylib.GetScreenHeight());

            //Seeker
            MathLibrary.Vector2 seekerPosition = new MathLibrary.Vector2(screenDimensions.x * 0.5f, screenDimensions.y * 0.75f);
            float seekerSpeed = 500;
            float seekerRadius = 10;
            float seekerViewAngle = 90;
            float seekerViewLeft = -90 - (seekerViewAngle / 2);
            float seekerViewRight = -90 + (seekerViewAngle / 2);
            float seekerViewDistance = 200;
            MathLibrary.Vector2 seekerForward = new MathLibrary.Vector2(0, 1).Normalized;

            //hider
            int hiderX = rnd.Next(100, 1500);
            int hiderY = rnd.Next(80, 880);
            
            MathLibrary.Vector2 hider1Position = new MathLibrary.Vector2(hiderX, hiderY * 0.25f);
            float hider1Radius = 10;
            Color hiderColor = Color.Black;
            int hiderFoundCount = -1;
            int hiderFoundTimer = 10000;

            while (!Raylib.WindowShouldClose())
            {
                //UPDATE
                
                //Print Hider found counter
                Raylib.DrawText("Hiders found: " + hiderFoundCount, 10, 10, 15, Color.White);

                //rehide Hider
                if (hiderFoundTimer == 10000)
                {
                    int hiderJumpscare = rnd.Next(7, 7);
                    if (hiderJumpscare == 7)
                    {
                        int jumpscaring = 0;
                        while (jumpscaring < 1000)
                        {

                            Texture2D jumpscare1 = Raylib.LoadTexture("downloads/Jumpscare1.png");
                            Raylib.DrawTexture(jumpscare1, hiderX, hiderY, Color.RayWhite);
                            jumpscaring++;
                        }
                    }

                    hiderX = rnd.Next(100, 1500);
                    hiderY = rnd.Next(80, 880);
                    Console.WriteLine(""+ hiderX +" | "+ hiderY);
                    hider1Position = new MathLibrary.Vector2(hiderX, hiderY);

                    hiderFoundCount++;
                    hiderFoundTimer = 0;
                }
                
                //Movement
                MathLibrary.Vector2 movementInput = new MathLibrary.Vector2();
                movementInput.y -= Raylib.IsKeyDown(KeyboardKey.W);
                movementInput.x -= Raylib.IsKeyDown(KeyboardKey.A);
                movementInput.y += Raylib.IsKeyDown(KeyboardKey.S);
                movementInput.x += Raylib.IsKeyDown(KeyboardKey.D);

                seekerPosition += movementInput * seekerSpeed * Raylib.GetFrameTime();

                //Calculate LOS
                MathLibrary.Vector2 seekerToHiderDirection = (seekerPosition - hider1Position).Normalized;
                float distance = hider1Position.Distance(seekerPosition);
                float angleTohider = (float)Math.Abs(seekerToHiderDirection.Angle(seekerForward));


                //If hider is in sight
                if (Math.Abs(angleTohider) < (seekerViewAngle / 2) * (Math.PI / 180) && distance < seekerViewDistance)
                {
                    hiderColor = Color.Red;
                    hiderFoundTimer++;
                }
                else
                {
                    hiderColor = Color.Black;
                }

                //DRAW
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.Black);

                //Draw hider
                Raylib.DrawCircleV(hider1Position, hider1Radius, hiderColor);

                //Draw Seeker
                Raylib.DrawCircleV(seekerPosition, seekerRadius, Color.Green);

                //Draw Seeker forward
                Raylib.DrawLineEx(seekerPosition, seekerPosition - (seekerForward * 100), 3, Color.Orange);

                //Draw view cone
                Raylib.DrawCircleSectorLines(seekerPosition,
                    seekerViewDistance,
                    seekerViewLeft,
                    seekerViewRight, 
                    10, Color.Blue);


                Raylib.EndDrawing();
            }
            Raylib.CloseWindow();





            /// <summary>
            /// Projectile
            /// </summary>
            Raylib.InitWindow(1600, 960, "Hello World");
            int speed = 5;
            int cleaveSpeed = 5;

            MathLibrary.Vector2 playerPosition = new MathLibrary.Vector2();
            MathLibrary.Vector2 playerSize = new MathLibrary.Vector2(10,10);
            MathLibrary.Vector2 playerVelocity = new MathLibrary.Vector2(1, 1) * speed;

            int xPosition = 100;
            int yPosition = 100;

            bool cleaveActivated = false;
            int cleaveLifetime = 0;
            MathLibrary.Vector2 cleavePosition = new MathLibrary.Vector2();
            MathLibrary.Vector2 cleaveSize = new MathLibrary.Vector2(10, 5);
            MathLibrary.Vector2 cleaveVelocity = new MathLibrary.Vector2(1, 1) * cleaveSpeed;
            while (!Raylib.WindowShouldClose())
            {
                //Draw Stuff
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.Black);
                Raylib.SetTargetFPS(60);

                //Draw player
                Raylib.DrawRectangleV(playerPosition, playerSize, Color.Red);

                //Get mousePosition and Draw the mouseCircle
                MathLibrary.Vector2 mousePosition = Raylib.GetMousePosition();
                MathLibrary.Vector2 mouseCirclePosition;
                mouseCirclePosition = mousePosition;
                Raylib.DrawCircleLinesV(mouseCirclePosition, 10, Color.Red);

                //Get player inputs
                int up = Raylib.IsKeyDown(KeyboardKey.W);
                int down = Raylib.IsKeyDown(KeyboardKey.S);
                int left = Raylib.IsKeyDown(KeyboardKey.A);
                int right = Raylib.IsKeyDown(KeyboardKey.D);
                
                //Change the Vector2 playerPosition based on player inputs
                playerPosition.y -= up * playerVelocity.y;
                playerPosition.y += down * playerVelocity.y;
                playerPosition.x -= left * playerVelocity.x;
                playerPosition.x += right * playerVelocity.x;

                //set cleaveActivated to true if player left clicks
                if (Raylib.IsMouseButtonPressed(MouseButton.Left))
                {
                    cleaveActivated = true;
                }

                //Move the cleave projectile as long as cleaveActivated is true
                if (cleaveActivated == true)
                {

                    if (cleaveLifetime == 0)
                    {
                        cleavePosition = new MathLibrary.Vector2(playerPosition.x, playerPosition.y);
                    }

                    //MathLibrary.Vector2 NormalizedMousePosition = MousePosition.Normalize();

                    //cleaveLine -= NormalizedMousePosition * speed;

                    //MathLibrary.Vector2 cleavePosition = cleaveLine;


                    cleavePosition += cleaveVelocity;


                    Raylib.DrawRectangleV(cleavePosition, cleaveSize, Color.White);


                    cleaveLifetime++;
                    if (cleaveLifetime == 25)
                    {
                        cleaveActivated = false;
                        cleaveLifetime = 0;
                    }
                }


                Raylib.EndDrawing();
            }
            Raylib.CloseWindow();





            /// <summary>
            /// Mouse attack
            /// </summary>
            Raylib.InitWindow(800, 480, "Hello World");
            Raylib.BeginDrawing();
            while (!Raylib.WindowShouldClose())
            {
                Raylib.GetFrameTime();
                
                

                MathLibrary.Vector2 mouseCirclePosition;



                mouseCirclePosition = Raylib.GetMousePosition();

                Raylib.DrawCircle(50, 50, 10, Color.Red);

                Raylib.DrawCircleLinesV(mouseCirclePosition, 5, Color.Red);

                if (Raylib.IsMouseButtonDown(MouseButton.Left))
                {

                    Raylib.DrawRectangle(20, 20, 20, 20, Color.Red);
                    
                    Raylib.DrawCircleV(mouseCirclePosition, 10, Color.Red);

                    Raylib.ClearBackground(Color.Black);
                }

                if (Raylib.IsMouseButtonReleased(MouseButton.Left))
                {

                    int circleXPosition = Raylib.GetMouseX();
                    int circleYPosition = Raylib.GetMouseY();


                    Raylib.DrawCircleV(mouseCirclePosition, 10, Color.Red);

                }

                Raylib.EndDrawing();
            }
            Raylib.CloseWindow();
        }
    }
}
