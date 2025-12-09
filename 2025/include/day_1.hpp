#pragma once
#include "day.hpp"

class Day1 : public Day {
public:
    string solvePartA(const string& input) override;
    string solvePartB(const string& input) override;
private:
    int getActionMovement(const string& action);
};

