

CREATE DATABASE dbolc2collections;

USE dbolc2collections;

// constans
String @GENRE_ELECTRONIC = "Electronic";
String @GENRE_ROCK = "Rock";


CREATE TYPE song {
    name String,
    trackNumber int,
    duration double,
    dateReleased Date,
    explicit boolean,
    genre String,
    album String,
    artist String
};

Song @something_about_us = {
    "Something about us",
    9,
    3.51,
    '2001-02-26',
    false,
    @GENRE_ELECTRONIC,
    "Discovery",
    "Daft Punk"
} as Song;

Song @taciturn = {
    "Taciturn",
    8,
    5.26,
    '2013-04-09',
    false,
    "Alternative Rock",
    "House of Gold & Bones Part 1",
    "Stone Sour"
} as Song;

Song whistle_for_the_choir = {
    "Whistle for the choir",
    1,
    3.37,
    '2006-11-27',
    false,
    "Indie Rock",
    "Whistle for the choir",
    "The Fratellis"
} as Song;

/* ----------------------------------------------------------------------------------------------------- */

CREATE TABLE playlists (
    id counter PRIMARY KEY,
    name String,
    songs Set<Song>,
    dateCreated Date,
    timeCreated Time
);

INSERT INTO playlists (name, songs, dateCreated, timeCreated)
VALUES(
    "Soft Rock",
    {
        {"It's probably Me", 1, 5.02, '2001-02-01', false, @GENRE_ROCK, "Fields of Gold", "Erick Clapton"} as Song,
        {"Bother", 2, 4.00, '2011-03-02', true, @GENRE_ROCK, "Stone Sour", "Stone Sour"} as Song,
        {"Strong Enough", 3, 4.00, '2012-03-24', false, @GENRE_ROCK, "Hits and Raritis", "Sheryl Crowl"} as Song,
        {"Purple Rain", 4, 8.41, '2012-03-17', false, @GENRE_ROCK, "Purple Rain", "Prince"} as Song,
        {"I surrender", 5, 3.39, '2013-03-18', false, @GENRE_ROCK, "These are the days", "Saybia"} as Song,
        {"Everlong", 6, 3.39, '2013-03-19', false, @GENRE_ROCK, "Greatest Hits", "Foo Fighters"} as Song,
        {"Wild Horses", 7, 3.39, '2014-03-02', false, @GENRE_ROCK, "Sticky Fingers", "The Rolling Stones"} as Song,
        {"Karma Police", 8, 4.22, '2014-03-02', false, @GENRE_ROCK, "The best of", "Radio Head"} as Song,
        {"Rain", 9, 3.27, '2015-03-02', false, @GENRE_ROCK, "We are not alone", "Breaking Benjamin"} as Song,
        {"Road Trippin", 9, 3.25, '2015-03-02', false, @GENRE_ROCK, "Californication", "Red Hot Chili Peppers"} as Song
    },
    '2019-08-25',
    '11:11:11'
);


INSERT INTO playlists (songs, name)
VALUES(
    {
        {"Evil Beauty", 1, 5.02, '2001-02-01', false, @GENRE_ELECTRONIC, "Reach for glory", "Black Mill"} as Song,
        {"Strobe", 2, 4.00, '2011-03-02', true, @GENRE_ELECTRONIC, "Strobe", "deadmau5"} as Song,
        {"Let it Be", 3, 4.00, '2012-03-24', false, @GENRE_ELECTRONIC, "Miracle", "Black Mill"} as Song,
        {"Epiphany", 4, 8.41, '2012-03-17', false, @GENRE_ELECTRONIC, "Epiphany", "TwoThirds"} as Song,
        {"Until We bleed", 5, 3.39, '2013-03-18', false, @GENRE_ELECTRONIC, "Kleerup", "Kleerup"} as Song,
        {"You've Got the love", 6, 3.39, '2013-03-19', false, @GENRE_ELECTRONIC, "You've got the love", "Florence"} as Song,
        {"Miracle", 7, 3.39, '2014-03-02', false, @GENRE_ELECTRONIC, "Miracle", "Black Mill"} as Song,
        {"Spirit of Life Police", 8, 4.22, '2014-03-02', false, @GENRE_ELECTRONIC, "Miracle", "Black Mill"} as Song,
        {"Oh That I Had", 9, 3.27, '2015-03-02', false, @GENRE_ELECTRONIC, "Meds EP", "Mt. Eden"} as Song,
        {"Pacifica", 9, 3.25, '2015-03-02', false, @GENRE_ELECTRONIC, "Pacifica EP", "Spor"} as Song
    },
    "chill"
);


UPDATE playlists
SET songs = songs + {
    @something_about_us,
    @taciturn
}
WHERE name == "Soft Rock";

UPDATE playlists
SET songs = songs + {
        'Jubel - Original Mix', 1, 4.44, '2001-01-01', false, @GENRE_ELECTRONIC 'Jubel', 'Klingande'
    };


printSongsByYear("Soft Rock", 2013);

procedure printSongsByYear(String @playlistName, int @year) {
    Cursor @c1 is CALL getPlaylist(@playlistName);

    OPEN @C1;

    Set @songs = @c1.songs;

    LOG( @playlistName +  " tiene las siguientes canciones del " + @year + ":");
    for (int @i = 0; @i < @songs.size(); @i++) {
        Song @song = @songs.gets(@1);

        if (@song.dateReleased.getYear() == @year) {
            LOG( @song.name + " del artista: " + @song.artist + " de su album " + @song.album);
        }
    }

    CLOSE @c1;
}

procedure getPlaylist(String @name) (Cursor @c1) {
    Cursor @c1 = SELECT songs from playlists where name == @name;
    return @c1;
}