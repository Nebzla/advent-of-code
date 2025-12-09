#pragma once
#include "day.hpp"
#include <utility>

class Day2 : public Day {
public:
    string solvePartA(const string& input) override;
    string solvePartB(const string& input) override;
private:
    vector<std::pair<long, long>> parseIDRanges(const string& input);
    
    bool isInvalidID(const long& ID);
    bool isInvalidRepeatingID(const long& ID);
};