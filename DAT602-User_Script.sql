DROP DATABASE if exists islandgamedb;
CREATE DATABASE islandgamedb;
USE islandgamedb;

DROP USER if exists 'joseph'@'localhost';
CREATE USER 'joseph'@'localhost' IDENTIFIED BY '11111';
GRANT ALL PRIVILEGES ON islandgamedb.* TO 'joseph'@'localhost';
FLUSH PRIVILEGES;

DELIMITER $$ 
DROP PROCEDURE IF EXISTS GenerateTables$$
CREATE PROCEDURE GenerateTables()
BEGIN 

    -- Drop Existing Tables
    DROP TABLE IF EXISTS Player, Enemy, Boss, PlayerCharacter, Inventory, Game, Session, Chest, Tile, Item, ItemType, Lobby, Chat, ChatSession;

    -- Create PlayerCharacter Table
    CREATE TABLE PlayerCharacter (
        CharacterID INT AUTO_INCREMENT PRIMARY KEY,
        PlayerID INT,
        GameID INT,
        `Row` INT DEFAULT 0,
        Col INT DEFAULT 0,
        Archetype VARCHAR(10),
        Health INT,
        Level INT,
        Kills INT DEFAULT 0,
        FOREIGN KEY (PlayerID) REFERENCES Player(PlayerID),
        FOREIGN KEY (GameID) REFERENCES Game(GameID),
        FOREIGN KEY (TileID) REFERENCES Tile(TileID)
    );
    

	-- Create Tile Table
    CREATE TABLE Tile (
        TileID INT AUTO_INCREMENT PRIMARY KEY,
        MapID INT,
        TileTypeID INT
    );

    -- Create Inventory Table
    CREATE TABLE Inventory (
        InventoryID INT AUTO_INCREMENT PRIMARY KEY,
        PlayerID INT,
        GameID INT,
        ItemTypeID INT,
        Quantity INT DEFAULT 1,
        FOREIGN KEY (PlayerID) REFERENCES Player(PlayerID),
        FOREIGN KEY (GameID) REFERENCES Game(GameID),
        FOREIGN KEY (ItemTypeID) REFERENCES ItemType(ItemTypeID)
    );

    -- Create Session Table
    CREATE TABLE `Session` (
        SessionID INT AUTO_INCREMENT PRIMARY KEY,
        PlayerID INT,
        GameID INT,
        StartTime DATETIME,
        EndTime DATETIME,
        SessionStatus ENUM('active', 'completed', 'terminated') NOT NULL,
        Score INT,
        FOREIGN KEY (PlayerID) REFERENCES Player(PlayerID),
        FOREIGN KEY (GameID) REFERENCES Game(GameID)
    );
	
    -- Create Map Table
	CREATE TABLE Map (
		MapID INT AUTO_INCREMENT PRIMARY KEY,
		GameID INT NOT NULL,
		TileID INT NOT NULL,
		MapType VARCHAR(50) NOT NULL, 
		MapNumber INT NOT NULL,
		FOREIGN KEY (GameID) REFERENCES Game(GameID), 
		FOREIGN KEY (TileID) REFERENCES Tile(TileID)  
    );
    
    -- Create Enemy Table
    CREATE TABLE Enemy (
        EnemyID INT AUTO_INCREMENT PRIMARY KEY,
        GameID INT,        
        TileID INT,
        Name VARCHAR(20) NOT NULL,
        Health INT,
        Damage INT
    );

    -- Create Boss Table
    CREATE TABLE Boss (
        BossID INT AUTO_INCREMENT PRIMARY KEY,
        GameID INT,
        Name VARCHAR(20) NOT NULL,
        TileID INT,
        Health INT,
        Damage INT
    );

    -- Create Chest Table
    CREATE TABLE Chest (
        ChestID INT AUTO_INCREMENT PRIMARY KEY,
        GameID INT,
        TileID INT,
        IsOpened BOOLEAN DEFAULT FALSE,
        FOREIGN KEY (GameID) REFERENCES Game(GameID),
        FOREIGN KEY (TileID) REFERENCES Tile(TileID)
    );

    
    -- Create Item Table
    CREATE TABLE Item (
        ItemID INT AUTO_INCREMENT PRIMARY KEY,
        ItemTypeID INT,
        ChestID INT,
        Name VARCHAR(50),
        Damage INT DEFAULT 0,
        Heal INT DEFAULT 20,
        FOREIGN KEY (ItemTypeID) REFERENCES ItemType(ItemTypeID),
        FOREIGN KEY (ChestID) REFERENCES Chest(ChestID)
    );

    -- Create ItemType Table
    CREATE TABLE ItemType (
        ItemTypeID INT AUTO_INCREMENT PRIMARY KEY,
        Archetype VARCHAR(50)
    );

    -- Create Chat Table
    CREATE TABLE Chat (
        ChatID INT AUTO_INCREMENT PRIMARY KEY,
        PlayerID INT,
        Time DATETIME,
        Text TEXT,
        FOREIGN KEY (PlayerID) REFERENCES Player(PlayerID)
    );

    -- Create ChatSession Table
    CREATE TABLE ChatSession (
        ChatSessionID INT AUTO_INCREMENT PRIMARY KEY,
        ChatID INT,
        SessionStart DATETIME,
        SessionEnd DATETIME,
        FOREIGN KEY (ChatID) REFERENCES Chat(ChatID)
    );

END$$
DELIMITER ;

SELECT 'Connected to the Database' as STATUS;

-- Create Player Table
    CREATE TABLE Player (
        PlayerID INT AUTO_INCREMENT PRIMARY KEY,
        Username VARCHAR(50) NOT NULL,
        UserPassword VARCHAR(50) NOT NULL,
        Email VARCHAR(100),
        LockedUser BOOLEAN DEFAULT FALSE,
        AdminUser BOOLEAN DEFAULT FALSE,
        LoginAttempts INT DEFAULT 0,
        UserOnline BOOLEAN DEFAULT FALSE,
        HighScore INT DEFAULT 0
    );
    
INSERT INTO Player (Username, UserPassword, Email, UserOnline) VALUES 
('Joseph', 'pass123', 'joseph@example.com', TRUE),
('Luffy', 'gumgum', 'luffy@example.com', TRUE),
('Zorro', 'santoryu', 'zorro@example.com', FALSE);

SELECT Username FROM Player WHERE UserOnline = TRUE;

-- Create Game Table
    CREATE TABLE Game (
        GameID INT AUTO_INCREMENT PRIMARY KEY,
        StartTime DATETIME NOT NULL,
        EndTime DATETIME,
        GameStatus ENUM('active', 'paused', 'completed') NOT NULL
    );
    
-- Create Game Board Table
    CREATE TABLE GameBoard (
    GameID INT,
    `Row` INT,
    Col INT,
    TileType ENUM('empty', 'player', 'enemy', 'item', 'chest') DEFAULT 'empty',
    PRIMARY KEY (GameID, `Row`, Col)
	);


-- Insert some games into the Game table
INSERT INTO Game (StartTime, GameStatus) VALUES 
(NOW(), 'active'),
(NOW() - INTERVAL 1 DAY, 'paused'),
(NOW() - INTERVAL 2 DAY, 'completed'),
(NOW() - INTERVAL 3 HOUR, 'active');

-- Query to get active games
DELETE FROM GameBoard WHERE GameID = 1;

SELECT GameID, StartTime FROM Game WHERE GameStatus = 'active';
SELECT * FROM GameBoard WHERE GameID = 1;


DROP TABLE IF EXISTS tblClickTarget;
CREATE TABLE tblClickTarget(
   UserName varchar(50) PRIMARY KEY,
   Password varchar(50) NULL,
   Attempts INT DEFAULT 0,
   LOCKED_OUT BOOL DEFAULT FALSE
);

-- Create tblPlayer table
    DROP TABLE IF EXISTS tblPlayer;
    CREATE TABLE tblPlayer(
        Name varchar(50) PRIMARY KEY,
        Password varchar(50) NOT NULL DEFAULT 'P@ssword',
        Attempts INT DEFAULT 0,
        LOCKED_OUT BOOL DEFAULT FALSE,
        TileID INT, 
        Strength INT DEFAULT 100,
        IsAdmin BOOL DEFAULT FALSE
    );

-- The CREATE TABLE and all other table specific DML could be put
-- into a PROCEDURE. I would expect you to do that for your Milestone One.
INSERT tblClickTarget( UserName, Password, Attempts, LOCKED_OUT)
VALUES ('JosephC','1221', 0, False),
       ('Luffy','1691', 0, False),
       ('Admin', 'AdminPass', 0, FALSE);

-- Put an admin user to tblPlayer for role checking
INSERT INTO tblPlayer(Name, Password, TileID, Strength, IsAdmin)
VALUES ('Admin', 'AdminPass', 1, 100, TRUE);

DROP PROCEDURE IF EXISTS RegisterUser;
DELIMITER $$
CREATE PROCEDURE RegisterUser(IN pEmail VARCHAR(50), IN pUserName VARCHAR(50), IN pPassword VARCHAR(50))
BEGIN
	-- Check if username or password is empty
    IF pEmail = '' OR pUserName = '' OR pPassword = '' THEN
        SELECT 'Field cannot be empty' AS MESSAGE;
    ELSE
    
    IF EXISTS (SELECT * FROM tblClickTarget WHERE Username = pUserName) THEN
        SELECT 'NAME EXISTS' AS MESSAGE;
    ELSE
        INSERT INTO tblClickTarget(UserName, Password) VALUES (pUserName, pPassword);
        SELECT 'ADDED USER NAME' AS MESSAGE;
    END IF;
    END IF;
END$$
DELIMITER ;

DROP PROCEDURE IF EXISTS Login;
DELIMITER $$

CREATE PROCEDURE Login(IN pUserName VARCHAR(50), IN pPassword VARCHAR(50))
COMMENT 'Check login'
BEGIN
    DECLARE numAttempts INT DEFAULT 0;
    DECLARE userIsAdmin BOOL DEFAULT FALSE;
    
    -- Check if the username or password is empty
    IF pUserName = '' OR pPassword = '' THEN
        SELECT 'Username and Password cannot be empty' AS Message, userIsAdmin AS IsAdmin;
    ELSE
        -- Check for valid login
        IF EXISTS (SELECT * 
                   FROM tblClickTarget
                   WHERE UserName = pUserName AND Password = pPassword) 
        THEN
            -- Reset Attempts to 0 and return "Logged In"
            UPDATE tblClickTarget 
            SET Attempts = 0
            WHERE UserName = pUserName;
            
            -- Check if user is an admin
            SELECT IsAdmin INTO userIsAdmin
            FROM tblPlayer
            WHERE Name = pUserName;
               
            SELECT 'Logged In' AS Message, userIsAdmin AS IsAdmin;
        ELSE 
            -- If the username exists, handle attempts and lockout
            IF EXISTS(SELECT * FROM tblClickTarget WHERE UserName = pUserName) THEN 
                SELECT Attempts 
                INTO numAttempts
                FROM tblClickTarget
                WHERE UserName = pUserName;
                
                SET numAttempts = numAttempts + 1;
                
                IF numAttempts > 5 THEN 
                    -- Lock out the user if attempts exceed 5
                    UPDATE tblClickTarget 
                    SET LOCKED_OUT = TRUE
                    WHERE UserName = pUserName;
                         
                    SELECT 'Locked Out' AS Message, userIsAdmin AS IsAdmin;
                ELSE
                    -- Increment attempts and return "Invalid username or password"
                    UPDATE tblClickTarget
                    SET Attempts = numAttempts
                    WHERE UserName = pUserName;
                        
                    SELECT 'Invalid username or password' AS Message, userIsAdmin AS IsAdmin;
                END IF;
            ELSE 
                -- If the username doesn't exist
                SELECT 'Invalid username or password' AS Message, userIsAdmin AS IsAdmin;
            END IF;
        END IF;
    END IF;              
END$$
DELIMITER ;


call Login('ToddC', '12345') ;
call Login('JosephC','1221');
call Login('Admin', 'AdminPass');

DROP PROCEDURE IF EXISTS GetGameGrid;
DELIMITER $$
CREATE PROCEDURE GetGameGrid(IN pGameID INT)
BEGIN
    SELECT `Row`, Col, TileType
    FROM GameBoard
    WHERE GameID = pGameID
    ORDER BY `Row`, Col;
END$$

DELIMITER ;


DROP PROCEDURE IF EXISTS UpdateGameGrid;
DELIMITER $$

CREATE PROCEDURE UpdateGameGrid(
    IN pGameID INT,
    IN pRow INT,
    IN pCol INT,
    IN pTileType VARCHAR(50)
)
BEGIN
    UPDATE GameBoard
    SET TileType = pTileType
    WHERE GameID = pGameID AND `Row` = pRow AND Col = pCol;
END$$

UPDATE GameBoard SET TileType = 'player' WHERE GameID = 1 AND `Row` = 0 AND Col = 0;
UPDATE GameBoard SET TileType = 'enemy' WHERE GameID = 1 AND `Row` = 4 AND Col = 4;
UPDATE GameBoard SET TileType = 'chest' WHERE GameID = 1 AND `Row` = 2 AND Col = 2;

DELIMITER ;

DROP PROCEDURE IF EXISTS CreateNewwGame;
DELIMITER $$
CREATE PROCEDURE CreateNewGame(IN pPlayerName VARCHAR(50))
BEGIN
    DECLARE newGameId INT;

    INSERT INTO Game (StartTime, GameStatus) VALUES (NOW(), 'active');
    SET newGameId = LAST_INSERT_ID();

    -- Optional: Link the new game to a player if necessary
    INSERT INTO Player (Username, UserPassword) VALUES (pPlayerName, 'defaultPassword');

    -- Return the new GameId
    SELECT newGameId AS GameId;
END$$
DELIMITER ;

CALL GetGameGrid(1);
CALL UpdateGameGrid(1, 2, 2, 'enemy');
CALL CreateNewGame('NewPlayer');

DROP PROCEDURE IF EXISTS UpdatePlayerPosition;
DELIMITER $$

DROP PROCEDURE IF EXISTS UpdatePlayerPosition$$
CREATE PROCEDURE UpdatePlayerPosition(
    IN pCharacterID INT,
    IN pNewRow INT,
    IN pNewCol INT
)
BEGIN
    UPDATE PlayerCharacter
    SET `Row` = pNewRow, Col = pNewCol
    WHERE CharacterID = pCharacterID;
END$$
DELIMITER ;

DROP PROCEDURE IF EXISTS InitializeGameBoard;
DELIMITER $$
CREATE PROCEDURE InitializeGameBoard(IN pGameID INT, IN pMaxRows INT, IN pMaxCols INT)
BEGIN
    DECLARE `row` INT DEFAULT 0;
    DECLARE col INT DEFAULT 0;

    -- Clear any existing board data for the game
    DELETE FROM GameBoard WHERE GameID = pGameID;

    -- Populate the board with empty tiles
    WHILE `row` < pMaxRows DO
        SET col = 0;
        WHILE col < pMaxCols DO
            INSERT INTO GameBoard (GameID, `Row`, Col, TileType) VALUES (pGameID, `row`, col, 'empty');
            SET col = col + 1;
        END WHILE;
        SET `row` = `row` + 1;
    END WHILE;

    -- Place a few sample elements on the board (for example, a player, enemy, and chest)
    UPDATE GameBoard SET TileType = 'player' WHERE GameID = pGameID AND `Row` = 0 AND Col = 0;
	UPDATE GameBoard SET TileType = 'enemy' WHERE GameID = pGameID AND `Row` = pMaxRows - 1 AND Col = pMaxCols - 1;
	UPDATE GameBoard SET TileType = 'chest' WHERE GameID = pGameID AND `Row` = FLOOR(pMaxRows / 2) AND Col = FLOOR(pMaxCols / 2);

END$$
DELIMITER ;


DROP PROCEDURE IF EXISTS MovePlayer;
DELIMITER $$
CREATE PROCEDURE MovePlayer(
    IN pGameID INT,
    IN pOldRow INT,
    IN pOldCol INT,
    IN pNewRow INT,
    IN pNewCol INT
)
BEGIN
    DECLARE tileType VARCHAR(10);

    -- Check if the new position is within bounds
    IF pNewRow >= 0 AND pNewCol >= 0 AND EXISTS (
        SELECT 1 FROM GameBoard WHERE GameID = pGameID AND `Row` = pNewRow AND Col = pNewCol
    ) THEN
        -- Check if the new tile is empty or can be entered
        SELECT TileType INTO tileType FROM GameBoard
        WHERE GameID = pGameID AND `Row` = pNewRow AND Col = pNewCol;

        IF tileType = 'empty' OR tileType = 'chest' THEN
            -- Move player to the new tile and update old tile to empty
            UPDATE GameBoard SET TileType = 'empty' WHERE GameID = pGameID AND `Row` = pOldRow AND Col = pOldCol;
            UPDATE GameBoard SET TileType = 'player' WHERE GameID = pGameID AND `Row` = pNewRow AND Col = pNewCol;
        END IF;
    END IF;
END$$
DELIMITER ;

DROP PROCEDURE IF EXISTS GetGameBoard;
DELIMITER $$

CREATE PROCEDURE GetGameBoard(IN pGameID INT)
BEGIN
    SELECT `Row`, Col, TileType FROM GameBoard WHERE GameID = pGameID ORDER BY `Row`, Col;
END$$
DELIMITER ;

-- Gameboard objects
INSERT INTO GameBoard (GameID, `Row`, Col, TileType) VALUES
(1, 0, 0, 'player'),  -- Player starting position
(1, 2, 2, 'enemy'),   -- enemy
(1, 3, 5, 'enemy'),   -- enemy
(1, 4, 7, 'enemy'),   -- enemy
(1, 4, 5, 'chest'),   -- chest
(1, 4, 9, 'chest'),   -- chest
(1, 6, 6, 'item'),   -- item
(1, 8, 6, 'item');    -- item





