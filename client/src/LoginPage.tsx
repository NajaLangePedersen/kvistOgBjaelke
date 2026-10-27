import {useState} from "react";
import { useNavigate } from "react-router-dom";
import apiClient from "@/ApiClient.ts";

export default function LoginPage(){
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState("");
    const navigate = useNavigate();

    const handleLogin = async (e: React.FormEvent) => {
        e.preventDefault();
        setError("");

        try {
            const response = await apiClient.api.userLogin({
                    username,
                    password,
            });

            localStorage.setItem("token",
                response.token);
            navigate("/users");
        } catch (err) {
            setError("Login failed");
        }
    }

    return (
        <form onSubmit={handleLogin}>
            <input
                type="text"
                placeholder="Username"
                value={username}
                onChange={(e) =>
                    setUsername(e.target.value)}
            />

            <input
                type="password"
                placeholder="Password"
                value={password}
                onChange={(e) =>
                    setPassword(e.target.value)}
            />

            <button type="submit">Login</button>

            {error && <p>{error}</p>}
        </form>
    );
}