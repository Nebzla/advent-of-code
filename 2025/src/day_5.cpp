#include "day_5.hpp"
#include "string_utils.hpp"
#include <algorithm>
#include <string>


vector<long> Day5::parseIDsToQuery(const string& text) {
    vector<string> strIDs = splitStringByLines(text);
    vector<long> IDs {};
    IDs.reserve(strIDs.size());

    for (string strID : strIDs) {
        IDs.push_back(std::stol(strID));
    }

    return IDs;
}

pair_v Day5::parseFreshIDRanges(const string& text) {
    vector<string> strRanges = splitStringByLines(text);
    pair_v ranges {};
    ranges.reserve(strRanges.size());

    for (string range : strRanges) {
        ranges.push_back(parseRange(range));
    }

    return ranges;
}

bool Day5::isIDInRange(const pair_l range, const long& ID) {
    return ID >= range.first && ID <= range.second;
}


string Day5::solvePartA(const string& input) {
    auto parts = splitStringByFirstOccurence(input, "\n\n");
    
    auto freshIDRanges = parseFreshIDRanges(parts.first);
    auto queryIDs = parseIDsToQuery(parts.second);

    int freshTotal = 0;
    for (auto ID : queryIDs) {
        for (auto IDRange : freshIDRanges) {
            if(isIDInRange(IDRange, ID)) {
                freshTotal ++;
                break;
            }
        }
    }
    
    return std::to_string(freshTotal);
}


void Day5::sortPairVector(pair_v& pairs) { // Orders a vector of pairs by the first value in ascending order
    auto comparator = [](pair_l a, pair_l b) {
        return a.first < b.first;
    };

    std::sort(pairs.begin(), pairs.end(), comparator);
}


// Sort ID ranges by smallest starting value. get range of first between start and end, and record end point
// Move to next ID range, and if start value is lower than end point of previous, start from that end and calculate difference again, etc

string Day5::solvePartB(const string& input) {
    auto parts = splitStringByFirstOccurence(input, "\n\n");
    pair_v IDRanges = parseFreshIDRanges(parts.first);
    
    sortPairVector(IDRanges);

    long previousEndValue = 0;
    long totalIDs = 0;

    for(pair_l IDRange : IDRanges) {
        long start = std::max(previousEndValue, IDRange.first); // Ensure no overlap
        long difference = IDRange.second - start + 1;

        if(difference > 0) {
            totalIDs += difference;
            previousEndValue = IDRange.second + 1;
        }
    }

    return std::to_string(totalIDs);
}