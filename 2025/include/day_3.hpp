#pragma once
#include "day.hpp"

class Day3 : public Day {
public:
    string solvePartA(const string& input) override;
    string solvePartB(const string& input) override;
private:
    string solve(const string& input, const int& batteries);

    vector<vector<short>> parseBatteryBanks(const string& input);
    std::pair<short, size_t> largestBattery(const vector<short>& bank, const size_t& start, const size_t& end);
    
    string getLargestJoltage(const vector<short>& bank, const int batteryCount);
};