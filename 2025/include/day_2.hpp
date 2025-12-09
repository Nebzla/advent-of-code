#pragma once
#include "day.hpp"
#include <utility>
#include <vector>

class Day2 : public Day {
public:
    std::string solvePartA(const std::string& input) override;
    std::string solvePartB(const std::string& input) override;
private:
    std::vector<std::pair<long, long>> parseIDRanges(const std::string& input);
    
    bool isInvalidID(const long& ID);
    bool isInvalidRepeatingID(const long& ID);
};