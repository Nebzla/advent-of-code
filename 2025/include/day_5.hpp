#pragma once
#include "day.hpp"

using pair_l = std::pair<long, long>;
using pair_v = vector<pair_l>;

class Day5 : public Day {
public:
    string solvePartA(const string& input) override;
    string solvePartB(const string& input) override;
private:
    vector<long> parseIDsToQuery(const string& text);
    pair_v parseFreshIDRanges(const string& text);
    
    bool isIDInRange(const pair_l range, const long& ID);

    void sortPairVector(pair_v& pairs);
};