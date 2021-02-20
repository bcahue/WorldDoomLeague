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
var WorldDoomLeague_1 = require("../../../WorldDoomLeague");
var state_1 = require("../../../state");
var MapList = function (props) {
    var _a = react_1.useState(false), loading = _a[0], setLoading = _a[1];
    var _b = react_1.useState([]), data = _b[0], setData = _b[1];
    var _c = react_1.useState(0), index = _c[0], setIndex = _c[1];
    var _d = react_1.useState(""), mapPack = _d[0], setMapPack = _d[1];
    var _e = react_1.useState(""), mapName = _e[0], setMapName = _e[1];
    var _f = react_1.useState(0), mapNumber = _f[0], setMapNumber = _f[1];
    var _g = react_1.useState(0), newMapId = _g[0], setNewMapId = _g[1];
    react_1.useEffect(function () {
        var fetchData = function () { return __awaiter(void 0, void 0, void 0, function () {
            var client, response, data_1, e_1;
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        setLoading(true);
                        _a.label = 1;
                    case 1:
                        _a.trys.push([1, 3, , 4]);
                        client = new WorldDoomLeague_1.MapsClient();
                        return [4 /*yield*/, client.get()
                                .then(function (response) { return response.toJSON(); })];
                    case 2:
                        response = _a.sent();
                        data_1 = response.mapList;
                        setData(data_1);
                        return [3 /*break*/, 4];
                    case 3:
                        e_1 = _a.sent();
                        state_1.setErrorMessage(JSON.parse(e_1.response));
                        return [3 /*break*/, 4];
                    case 4:
                        setLoading(false);
                        return [2 /*return*/];
                }
            });
        }); };
        fetchData();
    }, [newMapId]);
    var handleMapChange = function (name, value) {
        setIndex(value);
        props.update(name, value);
    };
    var handleSubmit = function (evt) { return __awaiter(void 0, void 0, void 0, function () {
        var client, command, response, e_2;
        return __generator(this, function (_a) {
            switch (_a.label) {
                case 0:
                    _a.trys.push([0, 2, , 3]);
                    client = new WorldDoomLeague_1.MapsClient();
                    command = new WorldDoomLeague_1.CreateMapCommand;
                    command.mapName = mapName;
                    command.mapNumber = mapNumber;
                    return [4 /*yield*/, client.create(command)];
                case 1:
                    response = _a.sent();
                    setNewMapId(response);
                    setMapPack('');
                    setMapName('');
                    setMapNumber(0);
                    return [3 /*break*/, 3];
                case 2:
                    e_2 = _a.sent();
                    state_1.setErrorMessage(JSON.parse(e_2.response));
                    return [3 /*break*/, 3];
                case 3: return [2 /*return*/];
            }
        });
    }); };
    // create a list for each engine.
    var renderMapDropdown = function () {
        var select = null;
        if (!loading) {
            if (data.length > 0) {
                select = (data.map(function (map) {
                    return React.createElement("option", { key: map.id, value: map.id },
                        map.mapName,
                        " | ",
                        map.mapPack);
                }));
            }
            else {
                select = (React.createElement("option", null, "No maps currently in the system."));
            }
        }
        else {
            select = (React.createElement("option", null, "Loading..."));
        }
        return (select);
    };
    // create a form for entering a new engine.
    var renderNewMapForm = function () {
        return (React.createElement(React.Fragment, null,
            React.createElement(reactstrap_1.FormGroup, null,
                React.createElement(reactstrap_1.Label, { for: "mapName" }, "Map Name"),
                React.createElement(reactstrap_1.Input, { type: "text", name: "mapName", id: "mapName", value: mapName, placeholder: "N's Base of Boppin'", onChange: function (e) { return setMapName(e.target.value); } })),
            React.createElement(reactstrap_1.FormGroup, null,
                React.createElement(reactstrap_1.Label, { for: "mapPack" }, "Pack"),
                React.createElement(reactstrap_1.Input, { type: "text", name: "mapPack", id: "mapPack", value: mapPack, placeholder: "32in24-4", onChange: function (e) { return setMapPack(e.target.value); } })),
            React.createElement(reactstrap_1.FormGroup, null,
                React.createElement(reactstrap_1.Label, { for: "engineUrl" }, "Map Number"),
                React.createElement(reactstrap_1.Input, { placeholder: "Amount", min: 1, max: 30, type: "number", step: "1", value: mapNumber, onChange: function (e) { return setMapNumber(parseInt(e.target.value, 10)); } })),
            React.createElement(reactstrap_1.Button, { color: "primary", size: "lg", block: true, disabled: !mapName || !mapName || !mapNumber, onClick: handleSubmit }, "Create New Map")));
    };
    return (React.createElement(React.Fragment, null,
        React.createElement(reactstrap_1.Form, null,
            React.createElement(reactstrap_1.FormGroup, null,
                React.createElement(reactstrap_1.Label, { for: "engine" }, "Map"),
                React.createElement(reactstrap_1.Input, { type: "select", name: "engine", id: "engineSelect", value: index, onChange: function (e) { return handleMapChange(e.target.name, e.target.value); } }, renderMapDropdown()))),
        React.createElement(reactstrap_1.Form, null, renderNewMapForm())));
};
exports.default = MapList;
//# sourceMappingURL=MapList.js.map