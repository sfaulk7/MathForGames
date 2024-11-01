using MathLibrary;
using Raylib_cs;
using System.ComponentModel;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace Sandbox
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Raylib.InitWindow(880, 480, "Hello World");
            Raylib.SetTargetFPS(60);

            Actor a = new Actor();

            Transform2D t1 = new Transform2D(a);
            t1.LocalScale = new MathLibrary.Vector2(100, 100);
            MathLibrary.Vector2 offset = new MathLibrary.Vector2(t1.LocalScale.x / 2, t1.LocalScale.y / 2);
            t1.LocalPosition = new MathLibrary.Vector2(
                (Raylib.GetScreenWidth() * 0.33f) - offset.x,
                (Raylib.GetScreenHeight() * 0.33f) - offset.y);

            Transform2D t2 = new Transform2D(a);
            t1.AddChild(t2);
            t2.LocalScale = new MathLibrary.Vector2(.5f, .5f);
            t2.LocalPosition = new MathLibrary.Vector2(
                (100) - offset.x,
                (50) - offset.y);

            while (!Raylib.WindowShouldClose())
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.White);

                t1.Translate(t1.Forward * 50 * Raylib.GetFrameTime());
                t1.Rotate(0.5f * Raylib.GetFrameTime());

                t2.Rotate(0.5f * Raylib.GetFrameTime());

                //Draw t1
                Rectangle rect = new Rectangle(t1.GlobalPosition + offset, t1.GlobalScale);
                Raylib.DrawRectanglePro(
                    rect,
                    new MathLibrary.Vector2(0,0) + offset,
                    -t1.GlobalRotationAngle * (180 / (float)Math.PI),
                    Color.Blue);

                //Draw t2
                rect = new Rectangle(t2.GlobalPosition + offset, t2.GlobalScale);
                Raylib.DrawRectanglePro(
                    rect,
                    new MathLibrary.Vector2(0, 0) + offset / 2,
                    -t2.GlobalRotationAngle * (180 / (float)Math.PI),
                    Color.Green);

                Raylib.DrawLineV(t1.GlobalPosition + offset, t1.GlobalPosition + offset + (t1.Forward * 100), Color.Red);
                Raylib.EndDrawing();
            }

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

            #region Luigi's Mansion: Dark Moon 3
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
            float seekerViewWidth = 90;
            float seekerViewLeft = -90 - (seekerViewWidth / 2);
            float seekerViewRight = -90 + (seekerViewWidth / 2);
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

            //Game
            while (!Raylib.WindowShouldClose())
            {
                //UPDATE

                //Find mouse position
                MathLibrary.Vector2 mousePosition = Raylib.GetMousePosition();

                //Print Hider found counter
                Raylib.DrawText("Hiders found: " + hiderFoundCount, 10, 10, 15, Color.White);

                //Rehide Hider
                if (hiderFoundTimer == 10000)
                {
                    hiderX = rnd.Next(100, 1500);
                    hiderY = rnd.Next(80, 880);
                    hider1Position = new MathLibrary.Vector2(hiderX, hiderY);

                    hiderFoundCount++;
                    hiderFoundTimer = 0;

                    Console.WriteLine("Hider is at x:" + hiderX + " y:" + hiderY);
                }

                //Movement
                MathLibrary.Vector2 movementInput = new MathLibrary.Vector2();
                movementInput.y -= Raylib.IsKeyDown(KeyboardKey.W);
                movementInput.x -= Raylib.IsKeyDown(KeyboardKey.A);
                movementInput.y += Raylib.IsKeyDown(KeyboardKey.S);
                movementInput.x += Raylib.IsKeyDown(KeyboardKey.D);

                seekerPosition += movementInput * seekerSpeed * Raylib.GetFrameTime();

                //Calculate LineOfSight
                MathLibrary.Vector2 seekerToHiderDirection = (seekerPosition - hider1Position).Normalized;
                float distance = hider1Position.Distance(seekerPosition);
                float angleTohider = (float)Math.Abs(seekerToHiderDirection.Angle(seekerForward));


                //If hider is in sight
                if (Math.Abs(angleTohider) < (seekerViewWidth / 2) * (Math.PI / 180) && distance < seekerViewDistance)
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

                Raylib.DrawLineV(seekerPosition, mousePosition, Color.White);

                Raylib.EndDrawing();
            }
            Raylib.CloseWindow();
            #endregion





            #region Projectile
            /// <summary>
            /// Projectile
            /// </summary>
            Raylib.InitWindow(1600, 960, "Hello World");
            int speed = 250;
            int cleaveSpeed = 5;

            MathLibrary.Vector2 playerPosition = new MathLibrary.Vector2();
            MathLibrary.Vector2 playerSize = new MathLibrary.Vector2(10, 10);
            MathLibrary.Vector2 playerVelocity = new MathLibrary.Vector2(1, 1) * speed;

            int xPosition = 100;
            int yPosition = 100;

            bool cleaveActivated = false;
            int cleaveLifetime = 0;
            MathLibrary.Vector2 cleavePosition = new MathLibrary.Vector2();
            MathLibrary.Vector2 cleaveTarget = new MathLibrary.Vector2();
            MathLibrary.Vector2 cleaveSize = new MathLibrary.Vector2(10, 5);
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

                //Movement
                MathLibrary.Vector2 movementInput = new MathLibrary.Vector2();
                movementInput.y -= Raylib.IsKeyDown(KeyboardKey.W);
                movementInput.x -= Raylib.IsKeyDown(KeyboardKey.A);
                movementInput.y += Raylib.IsKeyDown(KeyboardKey.S);
                movementInput.x += Raylib.IsKeyDown(KeyboardKey.D);

                playerPosition += movementInput * speed * Raylib.GetFrameTime();

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
                        cleaveTarget = mousePosition;
                        cleavePosition = new MathLibrary.Vector2(playerPosition.x, playerPosition.y);
                    }

                    float distance = cleaveTarget.Distance(cleavePosition);

                    float angle = cleaveTarget.Angle(cleavePosition);

                    Console.WriteLine(distance);

                    Console.WriteLine(angle);



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
            #endregion





            #region Mouse Attack
            /// <summary>
            /// Mouse attack
            /// </summary>
            Raylib.InitWindow(800, 480, "Hello World");
            screenDimensions =
                new MathLibrary.Vector2(Raylib.GetScreenWidth(), Raylib.GetScreenHeight());

            bool cleaveReleased = false;
            bool cleaveCut = false;
            bool cleaveEnded = true;
            bool spawnCube = true;
            MathLibrary.Vector2 cutCubePosition = new MathLibrary.Vector2(screenDimensions.x * 0.5f, screenDimensions.y * 0.5f);
            MathLibrary.Vector2 cutCubeSize = new MathLibrary.Vector2(50, 50);
            MathLibrary.Vector2 cutPoint1 = new MathLibrary.Vector2(1, 1);
            MathLibrary.Vector2 cutPoint2 = new MathLibrary.Vector2(1, 1);
            MathLibrary.Vector2 cleaveTargetEnd = new MathLibrary.Vector2(1, 1);

            MathLibrary.Vector2 cutCubeCollisionPosition = new MathLibrary.Vector2(cutCubePosition.x, cutCubePosition.y);

            MathLibrary.Vector2 fixer = new MathLibrary.Vector2(1, 1);

            while (!Raylib.WindowShouldClose())
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.Black);
                Raylib.SetTargetFPS(60);

                Console.WriteLine(cutPoint1);
                Console.WriteLine(cutPoint2);

                MathLibrary.Vector2 mousePosition = Raylib.GetMousePosition();
                Raylib.DrawCircleLinesV(mousePosition, 5, Color.Red);

                Raylib.DrawRectangleV(cutCubePosition, cutCubeSize, Color.Red);

                Raylib.DrawRectangleLines((int)cutCubeCollisionPosition.x, (int)cutCubeCollisionPosition.y, (int)cutCubeSize.x, (int)cutCubeSize.y, Color.Yellow);

                

                //Spawn Cube
                if (spawnCube == true)
                {
                    Raylib.DrawRectangleV(cutCubePosition, cutCubeSize, Color.Red);
                }

                spawnCube = Raylib.IsMouseButtonDown(MouseButton.Right);

                

                //Activate cleave
                if (cleaveActivated == true)
                {
                    
                    if (cleaveLifetime == 0)
                    {
                        cleaveTarget = mousePosition;
                        cutPoint1 = fixer;
                        cutPoint2 = fixer;
                    }

                    cleaveEnded = Raylib.IsMouseButtonDown(MouseButton.Left);

                    //Raylib.DrawLineV(cleaveTarget, mousePosition, Color.RayWhite);

                    int scaler = 2;
                    for (int i = 0; i < ((cutCubeSize.x + cutCubeSize.y) / 2) / scaler; i++)
                    {
                        
                        cleaveCut = false;

                        bool lineThroughCubeTop = false;
                        bool lineThroughCubeLeft = false;
                        bool lineThroughCubeRight = false;
                        bool lineThroughCubeBottom = false;

                        MathLibrary.Vector2 cutCubeCutPointTop = new MathLibrary.Vector2(cutCubeCollisionPosition.x + (scaler * i), cutCubeCollisionPosition.y);
                        MathLibrary.Vector2 cutCubeCutPointLeft = new MathLibrary.Vector2(cutCubeCollisionPosition.x, cutCubeCollisionPosition.y + (scaler * i));
                        MathLibrary.Vector2 cutCubeCutPointRight = new MathLibrary.Vector2(cutCubeCollisionPosition.x + cutCubeSize.x, cutCubeCollisionPosition.y + (scaler * i));
                        MathLibrary.Vector2 cutCubeCutPointBottom = new MathLibrary.Vector2(cutCubeCollisionPosition.x + (scaler * i), cutCubeCollisionPosition.y + cutCubeSize.y);

                        lineThroughCubeTop = Raylib.CheckCollisionPointLine(cutCubeCutPointTop, cleaveTarget, mousePosition, 1);
                        lineThroughCubeLeft = Raylib.CheckCollisionPointLine(cutCubeCutPointLeft, cleaveTarget, mousePosition, 1);
                        lineThroughCubeRight = Raylib.CheckCollisionPointLine(cutCubeCutPointRight, cleaveTarget, mousePosition, 1);
                        lineThroughCubeBottom = Raylib.CheckCollisionPointLine(cutCubeCutPointBottom, cleaveTarget, mousePosition, 1);

                        if (lineThroughCubeTop)
                            Raylib.DrawCircleV(cutCubeCutPointTop, scaler, Color.White);
                        else
                            Raylib.DrawCircleV(cutCubeCutPointTop, scaler, Color.Blue);

                        if (lineThroughCubeLeft)
                            Raylib.DrawCircleV(cutCubeCutPointLeft, scaler, Color.White);
                        else
                            Raylib.DrawCircleV(cutCubeCutPointLeft, scaler, Color.Blue);

                        if (lineThroughCubeRight)
                            Raylib.DrawCircleV(cutCubeCutPointRight, scaler, Color.White);
                        else
                            Raylib.DrawCircleV(cutCubeCutPointRight, scaler, Color.Blue);

                        if (lineThroughCubeBottom)
                            Raylib.DrawCircleV(cutCubeCutPointBottom, scaler, Color.White);
                        else
                            Raylib.DrawCircleV(cutCubeCutPointBottom, scaler, Color.Blue);

                        if (lineThroughCubeTop == true)
                        {
                            Raylib.DrawText("CUT TOP", 100, 100, 10, Color.White);
                            if (cleaveEnded == true)
                            {
                                if (cutPoint1 == fixer || cutCubeCutPointTop.y == cutPoint1.y)
                                {
                                    cutPoint1 = cutCubeCutPointTop;
                                }

                                if (cutPoint2.x - cutCubeCutPointTop.x > fixer.x && cutPoint2.y - cutCubeCutPointTop.y > fixer.y)  
                                {
                                    cutPoint2 = cutCubeCutPointTop;
                                } 
                            }
                        }

                        if (lineThroughCubeLeft == true)
                        {
                            Raylib.DrawText("CUT LEFT", 100, 120, 10, Color.White);
                            if (cleaveEnded == true)
                            {
                                if (cutPoint1 == fixer || cutCubeCutPointLeft.x == cutPoint1.x)
                                {
                                    cutPoint1 = cutCubeCutPointLeft;
                                }

                                if (cutPoint2.x - cutCubeCutPointLeft.x > fixer.x && cutPoint2.y - cutCubeCutPointLeft.y > fixer.y)
                                {
                                    cutPoint2 = cutCubeCutPointLeft;
                                }
                            }
                        }

                        if (lineThroughCubeRight == true)
                        {
                            Raylib.DrawText("CUT RIGHT", 100, 140, 10, Color.White);
                            if (cleaveEnded == true)
                            {
                                if (cutPoint1 == fixer || cutCubeCutPointRight.x == cutPoint1.x)
                                {
                                    cutPoint1 = cutCubeCutPointRight;
                                }

                                if (cutPoint2.x - cutCubeCutPointRight.x > fixer.x && cutPoint2.y - cutCubeCutPointRight.y > fixer.y)
                                {
                                    cutPoint2 = cutCubeCutPointRight;
                                }
                            }
                        }

                        if (lineThroughCubeBottom == true)
                        {
                            Raylib.DrawText("CUT BOTTOM", 100, 160, 10, Color.White);
                            if (cleaveEnded == true)
                            {
                                if (cutPoint1 == fixer || cutCubeCutPointBottom.y == cutPoint1.y)
                                {
                                    cutPoint1 = cutCubeCutPointBottom;
                                }

                                if (cutPoint2.x - cutCubeCutPointBottom.x > fixer.x && cutPoint2.y - cutCubeCutPointBottom.y > fixer.y)
                                {
                                    cutPoint2 = cutCubeCutPointBottom;
                                }
                            }
                        }
                        
                        if (cleaveEnded == false)
                        {
                            cleaveTargetEnd = mousePosition;
                            cleaveCut = true;
                            break;
                        }
                        
                    }

                    

                    cleaveLifetime++;
                }

                cleaveActivated = Raylib.IsMouseButtonDown(MouseButton.Left);

                //Reset cleave
                if (cleaveActivated == false && cleaveLifetime > 0)
                {
                    cleaveLifetime = 0;
                    cleaveReleased = true;
                }


                if (cleaveCut == true)
                {
                    if (cutPoint2 == fixer && cutPoint1 != fixer)
                    {
                        Raylib.DrawLineV(cutPoint1, cleaveTargetEnd, Color.White);
                    }
                    else
                    {
                        Raylib.DrawLineV(cutPoint1, cutPoint2, Color.White);
                    }
                }

                Raylib.EndDrawing();
            }
            Raylib.CloseWindow();
            #endregion
        }
    }
}
