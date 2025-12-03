#include "day_1.hpp"
#include "string_utils.hpp"
#include <string>

const int DIAL_LENGTH = 100;

int Day1::getActionMovement(const std::string& action) {
    char direction = action[0];
    int amount = std::stoi(action.substr(1));

    return direction == 'L' ? -amount : amount;
}

std::string Day1::solvePartA(const std::string& input) {
    std::vector<std::string> actions = splitStringByLines(input);
    int dialPosition = 50;
    int zeroCount = 0;

    for (std::string action : actions) {
        int movement = getActionMovement(action);
        dialPosition = (dialPosition + movement) % DIAL_LENGTH;

        if(dialPosition == 0) zeroCount ++;
    }

    return std::to_string(zeroCount);
}

std::string Day1::solvePartB(const std::string&input) {
    std::vector<std::string> actions = splitStringByLines(input);
    int dialPosition = 50;
    int zeroCount = 0;

    for (std::string action : actions) {
        int movement = getActionMovement(action);
        int fullCycles = abs(movement) / DIAL_LENGTH;
        int remainingMovement = movement % DIAL_LENGTH;

        zeroCount += fullCycles;
        
        int absPos = dialPosition + remainingMovement; // Position ignoring dial, using remainder of cycle not accounted for by full cycles
        int offsetPos = absPos % DIAL_LENGTH; // Offset from either end of dial
        int newPosition = offsetPos < 0 ? offsetPos + DIAL_LENGTH : offsetPos;
        bool hasCycled = absPos < 0 || absPos > DIAL_LENGTH;

        if (dialPosition != 0 && (hasCycled || newPosition == 0)) zeroCount ++;
        dialPosition = newPosition;
    }

    return std::to_string(zeroCount);
}