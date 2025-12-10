#include <day_4.hpp>
#include <string>
#include <utility>
#include "string_utils.hpp"

using std::make_pair, std::pair;

Vector2<bool> Day4::getFormattedGrid(const string& input) {    
    const vector<string> lines = splitStringByLines(input);
    Vector2<bool> grid(false, lines.size(), lines[0].size());

    for (size_t r = 0; r < lines.size(); r++) {
        for(size_t c = 0; c < lines[r].size(); c++) {
            grid.set(r, c, lines[r][c] == '@' ? true : false);
        }
    }

    return grid;
}

vector<pair<short, short>> directions = {
    make_pair(-1, -1), make_pair(-1, 0), make_pair(-1, 1),
    make_pair(0, -1), make_pair(0, 1),
    make_pair(1, -1), make_pair(1, 0), make_pair(1, 1)
};

bool Day4::isAccessible(const Vector2<bool>& grid, const size_t& row, const size_t& col) {
    ushort occupiedCount = 0;
    for (const auto direction : directions) {
        const long newRow = row + direction.first;
        const long newCol = col + direction.second;

        if(!grid.isCoordInBounds(newRow, newCol)) continue; // If out of bounds skip as wont be an obstruction
        if(grid[newRow][newCol] == true) occupiedCount++;
    }
    
    return occupiedCount < 4;
}

string Day4::solvePartA(const string& input) {
    auto grid = getFormattedGrid(input);

    uint accessibleSpots = 0;

    for (size_t r = 0; r < grid.cols(); r++) {
        for(size_t c = 0; c < grid.rows(); c++) {
            if(grid[r][c] == true && isAccessible(grid, r, c)) accessibleSpots++;
        }
    }

    return std::to_string(accessibleSpots);
}

string Day4::solvePartB(const string& input) {
    auto grid = getFormattedGrid(input);

    uint accessibleSpots = 0;
    bool hasChanged = true;

    while(hasChanged) { // Keep iterating until no more changes occur
        hasChanged = false;

        for (size_t r = 0; r < grid.cols(); r++) {
            for(size_t c = 0; c < grid.rows(); c++) {

                if(grid[r][c] == true && isAccessible(grid, r, c)) {
                    hasChanged = true;
                    grid.set(r, c, false);
                    accessibleSpots++;
                }
            }
        }
    }

    return std::to_string(accessibleSpots);
}