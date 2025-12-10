#pragma once
#include "day.hpp"
#include <utility>

class Day2 : public Day {
public:
    string solvePartA(const string& input) override;
    string solvePartB(const string& input) override;
private:    
    bool isInvalidID(const long& ID);
    bool isInvalidRepeatingID(const long& ID);
    vector<std::pair<long, long>> parseInput(const string& input);
};