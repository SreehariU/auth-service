import { useEffect, useState } from "react";
import api from "../api/axios";

export default function Dashboard() {
    const [message, setMessage] = useState("");

    useEffect(() => {
        api.get("/test")
            .then(res => setMessage(res.data))
            .catch(() => setMessage("Unauthorized"));
    }, []);

    return (
        <div>
            <h2>Dashboard</h2>
            <p>{message}</p>
        </div>
    );
}
