"use strict";
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var __generator = (this && this.__generator) || function (thisArg, body) {
    var _ = { label: 0, sent: function() { if (t[0] & 1) throw t[1]; return t[1]; }, trys: [], ops: [] }, f, y, t, g;
    return g = { next: verb(0), "throw": verb(1), "return": verb(2) }, typeof Symbol === "function" && (g[Symbol.iterator] = function() { return this; }), g;
    function verb(n) { return function (v) { return step([n, v]); }; }
    function step(op) {
        if (f) throw new TypeError("Generator is already executing.");
        while (_) try {
            if (f = 1, y && (t = op[0] & 2 ? y["return"] : op[0] ? y["throw"] || ((t = y["return"]) && t.call(y), 0) : y.next) && !(t = t.call(y, op[1])).done) return t;
            if (y = 0, t) op = [op[0] & 2, t.value];
            switch (op[0]) {
                case 0: case 1: t = op; break;
                case 4: _.label++; return { value: op[1], done: false };
                case 5: _.label++; y = op[1]; op = [0]; continue;
                case 7: op = _.ops.pop(); _.trys.pop(); continue;
                default:
                    if (!(t = _.trys, t = t.length > 0 && t[t.length - 1]) && (op[0] === 6 || op[0] === 2)) { _ = 0; continue; }
                    if (op[0] === 3 && (!t || (op[1] > t[0] && op[1] < t[3]))) { _.label = op[1]; break; }
                    if (op[0] === 6 && _.label < t[1]) { _.label = t[1]; t = op; break; }
                    if (t && _.label < t[2]) { _.label = t[2]; _.ops.push(op); break; }
                    if (t[2]) _.ops.pop();
                    _.trys.pop(); continue;
            }
            op = body.call(thisArg, _);
        } catch (e) { op = [6, e]; y = 0; } finally { f = t = 0; }
        if (op[0] & 5) throw op[1]; return { value: op[0] ? op[1] : void 0, done: true };
    }
};
Object.defineProperty(exports, "__esModule", { value: true });
var React = require("react");
var react_1 = require("react");
var reactstrap_1 = require("reactstrap");
var state_1 = require("../../../state");
var WorldDoomLeague_1 = require("../../../WorldDoomLeague");
var SeasonList = function () {
    var _a = react_1.useState(false), loading = _a[0], setLoading = _a[1];
    var _b = react_1.useState([]), data = _b[0], setData = _b[1];
    react_1.useEffect(function () {
        var fetchData = function () { return __awaiter(void 0, void 0, void 0, function () {
            var client, response, data_1, e_1;
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        setLoading(true);
                        _a.label = 1;
                    case 1:
                        _a.trys.push([1, 4, , 5]);
                        client = new WorldDoomLeague_1.SeasonsClient();
                        return [4 /*yield*/, client.get()
                                .then(function (response) { return response.toJSON(); })];
                    case 2:
                        response = _a.sent();
                        return [4 /*yield*/, response.seasonList];
                    case 3:
                        data_1 = _a.sent();
                        setData(data_1);
                        return [3 /*break*/, 5];
                    case 4:
                        e_1 = _a.sent();
                        state_1.setErrorMessage("Error: " + e_1);
                        return [3 /*break*/, 5];
                    case 5:
                        setLoading(false);
                        return [2 /*return*/];
                }
            });
        }); };
        fetchData();
    }, []);
    // create a list for each season.
    var renderSeasonList = function () {
        var seasonList = data;
        var listArray = [];
        if (!loading) {
            var seasonObject = seasonList;
            if (seasonList.length > 0) {
                seasonObject.forEach(function (value) {
                    listArray.push(React.createElement(reactstrap_1.ListGroupItem, null, value.seasonName));
                });
            }
            else {
                listArray.push(React.createElement(reactstrap_1.ListGroupItem, null, "There are currently no seasons recorded in the system."));
            }
        }
        else {
            listArray.push(React.createElement(reactstrap_1.ListGroupItem, null,
                React.createElement(reactstrap_1.Spinner, { size: "sm", color: "primary" })));
        }
        return (listArray);
    };
    return (React.createElement(React.Fragment, null,
        React.createElement(reactstrap_1.ListGroup, null, renderSeasonList())));
};
exports.default = SeasonList;
//# sourceMappingURL=SeasonList.js.map