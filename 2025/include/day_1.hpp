#pragma once
#include "day.hpp"

class Day1 : public Day {
public:
    std::string solvePartA(const std::string& input) override;
    std::string solvePartB(const std::string& input) override;
private:
    int getActionMovement(const std::string& action);
};

