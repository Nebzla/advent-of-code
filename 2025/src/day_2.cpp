#include "day_2.hpp"
#include "string_utils.hpp"
#include "math_utils.hpp"

#include <regex>
#include <string>


// If it's made up *only* of digits repeated twice if invalid, then can identify a combination from start, not at any position
// Shouldn't need to create a combination more than half the length of the id

// A sequence can only be invalid if the first half of the sequence equals the second half
// A sequence cannot possibly be invalid if it has an odd number of digits

bool Day2::isInvalidID(const long& ID) {
    string IDStr = std::to_string(ID);
    if(IDStr.length() % 2 != 0) return false; // If an odd length, cannot be a symmetric ID

    int midpoint = IDStr.length() / 2;
    return IDStr.substr(0, midpoint) == IDStr.substr(midpoint);
}


std::regex invalidIDRegex ("(\\d+)\\1{1,}");
bool Day2::isInvalidRepeatingID(const long& ID) {
    
    string IDStr =  std::to_string(ID);
    return std::regex_match(IDStr, invalidIDRegex);
}

vector<std::pair<long, long>> Day2::parseInput(const string& input) {
    vector<std::pair<long, long>> ranges {};
    vector<string> strRanges = splitStringByDelimiter(input, ',');

    for (const string strRange : strRanges) {
        ranges.push_back(parseRange(strRange));
    }

    return ranges;
}


string Day2::solvePartA(const string& input) {
    auto ranges = parseInput(input);
    long IDSum = 0;
    
    for (const auto range : ranges) {
        for (long n = range.first; n <= range.second; n++) {
            if(isInvalidID(n)) IDSum += n;
        }
    }

    return std::to_string(IDSum);
}

string Day2::solvePartB(const string& input) {
    auto ranges = parseInput(input);
    long IDSum = 0;
    
    for (const auto range : ranges) {
        for (long n = range.first; n <= range.second; n++) {
            if(isInvalidRepeatingID(n)) IDSum += n;
        }
    }

    return std::to_string(IDSum);
}

